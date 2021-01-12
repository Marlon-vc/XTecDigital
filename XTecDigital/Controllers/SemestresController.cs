using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Helpers;
using XTecDigital.Models;
using XTecDigital.Models.Requests;

namespace XTecDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemestresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SemestresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Semestres
        [HttpGet]
        public IActionResult GetSemestres() {
            return Ok();
        }

        //GET: api/Semestres/5
        [HttpGet("{id}")]
        public IActionResult GetSemestre(int id) {
            return Ok();
        }

        //POST: api/Semestres
        [HttpPost]
        public async Task<IActionResult> AddSemestreAsync(Models.Requests.SemestreInfo data) {
            
            if (data == null || data.Grupos == null) {
                return BadRequest();
            }

            if (SemestreExists(data.IdPeriodo, data.Anio))
                return Conflict();

            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try {
                //crear semestre
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXECUTE dbo.sp_create_semester {data.Anio}, {data.IdPeriodo}
                ");
                await _context.SaveChangesAsync();

                //obtener id del semestre actual
                var idSemestre = _context.Semestre.FromSqlInterpolated($@"
                    EXECUTE dbo.sp_get_semestre {data.IdPeriodo}, {data.Anio}
                ").AsEnumerable().FirstOrDefault().Id;

                foreach (var grupo in data.Grupos)
                {
                    //Agregar el actual al semestre
                    await _context.Database.ExecuteSqlInterpolatedAsync($@" 
                        EXECUTE dbo.sp_create_curso_semestre {grupo.IdCurso}, {idSemestre}
                    ");   
                    await _context.SaveChangesAsync();

                    //Crear el grupo
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        EXECUTE dbo.sp_create_grupo {grupo.Numero}, {grupo.IdCurso}
                    ");
                    await _context.SaveChangesAsync();

                    var idGrupo = _context.Grupo.FromSqlInterpolated($@"
                        EXECUTE dbo.sp_get_grupo {grupo.Numero}, {grupo.IdCurso}
                    ").AsEnumerable().FirstOrDefault().Id;

                    //Agregar estudiantes
                    foreach (var estudiante in grupo.Estudiantes)
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            EXECUTE dbo.sp_create_grupo_estudiante {grupo.Numero}, {estudiante}
                        "); 
                        await _context.SaveChangesAsync();
                    }

                    //Agregar profesores
                    foreach(var profesor in grupo.Profesores) 
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            EXECUTE dbo.sp_create_grupo_profesor {grupo.Numero}, {profesor}
                        "); 
                        await _context.SaveChangesAsync();
                    }

                    //Agregar carpetas
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        dbo.sp_create_initial_folders {idGrupo};
                    ");
                    //Carpeta de grupo
                    var carpetaGrupo = FileHandler.GetGroupFolder(idGrupo);
                    Directory.CreateDirectory(carpetaGrupo);
                    //Carpeta de documentos
                    var documentos = Path.Combine(carpetaGrupo, "Documentos");
                    Directory.CreateDirectory(documentos);
                    //Carpeta de entregables
                    var entregables = Path.Combine(carpetaGrupo, "Entregables");
                    Directory.CreateDirectory(entregables);
                    //Carpeta de evaluaciones
                    var evaluaciones = Path.Combine(carpetaGrupo, "Evaluaciones");
                    Directory.CreateDirectory(evaluaciones);

                    //Agregar rubros
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        EXECUTE dbo.sp_create_initial_rubro {idGrupo}
                    ");

                    await _context.SaveChangesAsync();

                }
                
                await dbTransaction.CommitAsync();
            } catch (DbUpdateException ex) {
                return StatusCode(500, ex.Message);
            }

            return Ok();
        }

        private bool SemestreExists(int idPeriodo, int anio)
        {
            var result = _context.Semestre.FromSqlInterpolated(
                $"EXECUTE dbo.sp_get_semestre {idPeriodo}, {anio}"
            ).AsEnumerable();
            return result.Any();
        }
    }
}