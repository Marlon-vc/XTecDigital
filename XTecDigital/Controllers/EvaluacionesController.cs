using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Models;
using XTecDigital.Models.Dtos;
using XTecDigital.Models.Requests;
using XTecDigital.Helpers;
using System.IO;
using System;

namespace XTecDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EvaluacionesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET: api/Evaluaciones/Rubro
        [HttpGet("Rubro")]
        public async Task<IActionResult> GetEvaluacionesRubroAsync([FromQuery] RubroDto rubro)
        {
            var result = await _context.Evaluacion.FromSqlInterpolated($@"
                dbo.sp_get_evaluaciones_rubro {rubro.Nombre}, {rubro.Numero}, {rubro.Curso}, {rubro.Anio}, {rubro.Periodo}
            ").ToListAsync();

            return Ok(_mapper.Map<List<EvaluacionDto>>(result));
        }

        //GET: api/Evaluaciones/Info
        [HttpGet("Info")]
        public async Task<IActionResult> GetInfoEvaluacionAsync([FromQuery] EvaluacionInfo info)
        {
            var result = (await _context.InfoEvaluacion.FromSqlInterpolated($@"
                dbo.sp_get_info_evaluacion {info.Nombre}, {info.Rubro}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}, {info.Estudiante}
            ").ToListAsync()).FirstOrDefault();

            return Ok(result);
        }

        [HttpGet("eval-prof")]
        public async Task<IActionResult> GetEvaluacionesProfAsync([FromQuery] EvaluacionProfInfo info)
        {
            var result = await _context.InfoEvaluacion.FromSqlInterpolated($@"
                dbo.sp_get_info_evaluaciones_prof {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}, {info.Profesor}
            ").ToListAsync();

            return Ok(result);
        }

        [HttpGet("integrantes/{idGrupo}")]
        public async Task<IActionResult> GetIntegrantesGrupoAsync(int idGrupo)
        {
            var result = await _context.EvaluacionIntegrantes.FromSqlInterpolated($@"
                dbo.sp_get_students_in_group {idGrupo}
            ").ToListAsync();

            return Ok(result);
        }

        [HttpGet("evaluar")]
        public async Task<IActionResult> GetInfoEvaluarAsync([FromQuery] InfoEvaluar info) 
        {
            var result = await _context.InfoEvaluarEntregables.FromSqlInterpolated($@"
                dbo.sp_get_info_evaluar {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}, 
                {info.Profesor}, {info.Evaluacion}, {info.Rubro}
            ").ToListAsync();

            return Ok(result);
        }

        [HttpPost("entregable/{idGrupo}")]
        public async Task<IActionResult> PostEntregableAsync(int idGrupo, UploadInfo info)
        {
            if (string.IsNullOrWhiteSpace(info.FileData))
                return BadRequest();

            var fileName = info.Name.CoerceValidFileName();

            var data = FileHandler.FromBase64String(info.FileData);

            var groupFolder = FileHandler.GetGroupFolder(info.Numero, info.Curso, info.Anio, info.Periodo);

            var filePath = Path.Combine(groupFolder, info.Carpeta, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, data);

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_file {fileName}, {DateTime.Now}, {info.Size}, {info.Carpeta}, {info.TipoCarpeta}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
            ");

            var file = (await _context.Archivo.FromSqlInterpolated($@"
                dbo.sp_get_file {fileName}, {info.Carpeta}, {info.TipoCarpeta}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
            ").ToListAsync()).FirstOrDefault();

            //TODO: actualizar evaluacion_grupo para registrar entregable
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_update_grupo_with_entregable {idGrupo}, {file.Nombre}, {file.Carpeta}, {file.TipoCarpeta}
            ");

            return CreatedAtRoute("Default", file);
        }
        
        [HttpPut("guardar")]
        public async Task<IActionResult> UpdateInfoEvaluacion(List<UpdateEvaluacionGrupoInfo> info)
        {
            if (info == null)
                return BadRequest();

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var eval in info)
                {
                    //Crear detalle
                    if (string.IsNullOrWhiteSpace(eval.FileData))
                        return BadRequest();
                    
                    var fileName = eval.Detalle.CoerceValidFileName();
                    var data = FileHandler.FromBase64String(eval.FileData);
                    var groupFolder = FileHandler.GetGroupFolder(eval.Numero, eval.Curso, eval.Anio, eval.Periodo);
                    string folder = "Detalles";
                    string tipoCarpeta = "DETALLES";

                    var filePath = Path.Combine(groupFolder, folder, fileName);
                    await System.IO.File.WriteAllBytesAsync(filePath, data);

                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        dbo.sp_create_file {fileName}, {DateTime.Now}, {eval.Size}, {folder}, {tipoCarpeta}, {eval.Numero}, {eval.Curso}, {eval.Anio}, {eval.Periodo}
                    ");

                    await _context.SaveChangesAsync();
                    
                    //Modificar la informacion
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        dbo.sp_update_evaluacion_grupo {eval.IdEvaluacionGrupo}, {eval.Nota}, {eval.Observaciones}, {eval.Detalle}
                    ");
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex);
            }
            
            

            return Ok();
            
            
        }

        [HttpPut("publicar")]
        public async Task<IActionResult> SetNotasPublicas(EvaluacionNotas info) 
        {
            if (info == null)
                return BadRequest();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_set_notas_publicas {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}, {info.Nombre}, {info.Rubro}
            ");

            return Ok();
        }

        [HttpPost("asignacion")]
        public async Task<IActionResult> AsignarEvaluacion(AsignacionInfo info)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Crear el archivo
                if (string.IsNullOrWhiteSpace(info.FileData))
                    return BadRequest();

                var fileName = info.NombreEspec.CoerceValidFileName();
                var data = FileHandler.FromBase64String(info.FileData);
                var groupFolder = FileHandler.GetGroupFolder(info.Numero, info.Curso, info.Anio, info.Periodo);
                string folder = "Especificaciones";
                string tipoCarpeta = "ESPECIFICACIONES";

                var filePath = Path.Combine(groupFolder, folder, fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, data);

                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    dbo.sp_create_file {fileName}, {DateTime.Now}, {info.Size}, {folder}, {tipoCarpeta}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
                ");

                await _context.SaveChangesAsync();

                //Crear la evaluacion
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    dbo.sp_create_evaluation {info.NombreEvaluacion}, {info.FechaEntrega}, {info.PesoNota}, {info.Grupal}, {info.NombreEspec},
                    {info.Rubro}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
                ");
                await _context.SaveChangesAsync();

                //Crear la evaluacion grupo

                if (info.Grupal)
                {
                    foreach (var grupo in info.Estudiantes)
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            dbo.sp_create_evaluation_group {info.NombreEvaluacion}, {info.Rubro}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
                        ");
                        await _context.SaveChangesAsync();

                        var evaluacionGrupo = (await _context.EvaluacionGrupo.FromSqlInterpolated($@"
                            dbo.sp_get_inserted_grupo
                        ").ToListAsync()).FirstOrDefault();

                        foreach (var estudiante in grupo)
                        {
                            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                                dbo.sp_create_evaluation_student {evaluacionGrupo.Id}, {estudiante}
                            ");
                            await _context.SaveChangesAsync();
                        }

                    }
                }
                else
                {
                    var students = await _context.EstudianteGrupo.FromSqlInterpolated($@"
                        dbo.get_students_group {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
                    ").ToListAsync();

                    foreach (var student in students)
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            dbo.sp_create_evaluation_group {info.NombreEvaluacion}, {info.Rubro}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
                        ");
                        await _context.SaveChangesAsync();

                        var evaluacion = (await _context.EvaluacionGrupo.FromSqlInterpolated($@"
                            dbo.sp_get_inserted_grupo
                        ").ToListAsync()).FirstOrDefault();

                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            dbo.sp_create_evaluation_student {evaluacion.Id}, {student.Estudiante}
                        ");
                        await _context.SaveChangesAsync();
                    }
                }

                await transaction.CommitAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex);
            }

            return Ok();
        }

    }
}