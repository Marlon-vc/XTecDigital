using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using XTecDigital.Helpers;
using XTecDigital.Models;

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

        [HttpPost]
        public async Task<IActionResult> AddSemesterAsync(Models.Requests.SemestreInfo data)
        {
            if (data == null || string.IsNullOrWhiteSpace(data.Periodo) || data.Grupos == null)
                return BadRequest();

            if (SemestreExists(data.Periodo, data.Anio))
                return Conflict();
            
            using var transaction = await _context.Database.BeginTransactionAsync();

            try {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    dbo.sp_create_semester {data.Anio}, {data.Periodo}
                ");
                await _context.SaveChangesAsync();

                foreach (var grupo in data.Grupos)
                {
                    // Crear grupo
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        dbo.sp_create_grupo {grupo.Numero}, {grupo.Curso}, {data.Anio}, {data.Periodo}
                    ");
                    await _context.SaveChangesAsync();

                    // Crear grupo_estudiante
                    foreach (var estudiante in grupo.Estudiantes)
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            dbo.sp_create_grupo_estudiante {grupo.Numero}, {grupo.Curso}, {data.Anio}, {data.Periodo}, {estudiante}
                        ");
                    }
                    await _context.SaveChangesAsync();

                    // Crear grupo_profesor
                    foreach (var profesor in grupo.Profesores)
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            dbo.sp_create_grupo_profesor {grupo.Numero}, {grupo.Curso}, {data.Anio}, {data.Periodo}, {profesor}
                        ");
                    }
                    await _context.SaveChangesAsync();

                    // Crear carpetas iniciales
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        dbo.sp_create_initial_folders {grupo.Numero}, {grupo.Curso}, {data.Anio}, {data.Periodo}
                    ");
                    await _context.SaveChangesAsync();

                    var carpetas = await _context.Carpeta.FromSqlInterpolated($@"
                        dbo.sp_get_all_group_folders {grupo.Numero}, {grupo.Curso}, {data.Anio}, {data.Periodo}
                    ").ToListAsync();

                    var carpetaGrupo = FileHandler.GetGroupFolder(grupo.Numero, grupo.Curso, data.Anio, data.Periodo);
                    foreach (var carpeta in carpetas)
                    {
                        Directory.CreateDirectory(Path.Combine(carpetaGrupo, carpeta.Nombre));
                    }

                    // Crear rubros por defecto
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        dbo.sp_create_initial_rubro {grupo.Numero}, {grupo.Curso}, {data.Anio}, {data.Periodo}
                    ");
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

            } catch (DbUpdateException ex)
            {
                return StatusCode(500, ex);
            }

            return Ok();
        }

        private bool SemestreExists(string periodo, int anio)
        {
            return _context.Semestre.FromSqlInterpolated(
                $"EXECUTE dbo.sp_get_semestre {anio}, {periodo}"
            ).ToList().Any();
        }
    }
}