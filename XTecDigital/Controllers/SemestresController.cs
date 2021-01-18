using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Helpers;
using XTecDigital.Models;
using XTecDigital.Models.Requests;
using ExcelDataReader;
using System.Text;
using System.Data;
using System.Collections.Generic;

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

            if (SemestreExists(data.Periodo, data.Anio))
                return Conflict();

            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try {
                //crear semestre
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXECUTE dbo.sp_create_semester {data.Anio}, {data.Periodo}
                ");
                await _context.SaveChangesAsync();

                //obtener id del semestre actual
                var idSemestre = _context.Semestre.FromSqlInterpolated($@"
                    EXECUTE dbo.sp_get_semestre {data.Periodo}, {data.Anio}
                ").AsEnumerable().FirstOrDefault().Id;

                foreach (var grupo in data.Grupos)
                {
                    //Agregar el actual al semestre
                    // await _context.Database.ExecuteSqlInterpolatedAsync($@" 
                    //     EXECUTE dbo.sp_create_curso_semestre {grupo.IdCurso}, {idSemestre}
                    // ");   
                    // await _context.SaveChangesAsync();

                    //Crear el grupo
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        EXECUTE dbo.sp_create_grupo {grupo.Numero}, {grupo.IdCurso}, {idSemestre}
                    ");
                    await _context.SaveChangesAsync();

                    var idGrupo = _context.Grupo.FromSqlInterpolated($@"
                        EXECUTE dbo.sp_get_grupo {grupo.Numero}, {grupo.IdCurso}, {idSemestre}
                    ").AsEnumerable().FirstOrDefault().Id;

                    //Agregar estudiantes
                    foreach (var estudiante in grupo.Estudiantes)
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            EXECUTE dbo.sp_create_grupo_estudiante {idGrupo}, {estudiante}
                        "); 
                        await _context.SaveChangesAsync();
                    }

                    //Agregar profesores
                    foreach(var profesor in grupo.Profesores) 
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            EXECUTE dbo.sp_create_grupo_profesor {idGrupo}, {profesor}
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

        [HttpPost("file")]
        public IActionResult AddSemestreExcel([FromBody]String data) {
            string filePath = "C:/Users/pvill/Desktop/Proyecto.xlsx";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // using var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);
            // using var reader = ExcelReaderFactory.CreateReader(stream);
            // var result = reader.AsDataSet();

            
            // // // Ejemplos de acceso a datos
            // DataTable table = result.Tables[0];
            // DataRow row = table.Rows[0];
            // table.Rows.Count;
            // string cell = row[0].ToString();
            // Console.WriteLine(row.ToString());

            List<SemestreExcel> cursos = new List<SemestreExcel>();
            var count = 0;

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                         while (reader.Read()){
                             if (count == 0) {
                                count++;
                                continue;
                            }

                            if (reader.GetString(0) == null) {
                                break;
                            }
                            SemestreExcel sem = new SemestreExcel
                            {
                                Carnet = reader.GetString(0),
                                Nombre = reader.GetString(1),
                                Apellido1 = reader.GetString(2),
                                Apellido2 = reader.GetString(3),
                                IdCurso = reader.GetString(4),
                                NombreCurso = reader.GetString(5),
                                Anio = reader.GetDouble(6),
                                Periodo = reader.GetDouble(7),
                                Grupo = reader.GetDouble(8),
                                IdProfesor = reader.GetString(9),
                                NombreProfesor = reader.GetString(10),
                                Apellido1Profesor = reader.GetString(11),
                                Apellido2Profesor = reader.GetString(12)
                            };


                            cursos.Add(sem);
                            Console.WriteLine(cursos.Count());
                        }
                    } while (reader.NextResult());
                }
            }

            Console.WriteLine(cursos.Count());
            
            return Ok();
        }

        
        private bool SemestreExists(string periodo, int anio)
        {
            var result = _context.Semestre.FromSqlInterpolated(
                $"EXECUTE dbo.sp_get_semestre {periodo}, {anio}"
            ).AsEnumerable();
            return result.Any();
        }
    }
}