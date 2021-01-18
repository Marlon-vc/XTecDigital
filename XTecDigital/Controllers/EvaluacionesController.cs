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
        public async Task<IActionResult> GetEvaluacionesRubroAsync(Rubro rubro)
        {
            var result = await _context.Evaluacion.FromSqlInterpolated($@"
                dbo.sp_get_evaluaciones_rubro {rubro.Nombre}, {rubro.Numero}, {rubro.Curso}, {rubro.Anio}, {rubro.Periodo}
            ").ToListAsync();

            return Ok(_mapper.Map<List<EvaluacionDto>>(result));
        }

        //GET: api/Evaluaciones/Info
        [HttpGet("Info")]
        public async Task<IActionResult> GetInfoEvaluacionAsync(EvaluacionInfo info)
        {
            var result = (await _context.InfoEvaluacion.FromSqlInterpolated($@"
                dbo.sp_get_info_evaluacion {info.Nombre}, {info.Rubro}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}, {info.Estudiante}
            ").ToListAsync()).FirstOrDefault();

            return Ok(result);
        }

        // [HttpGet("Evaluaciones")]
        // public async Task<IActionResult> GetEvaluacionesProfAsync(EvaluacionProfInfo info) {}

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
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    dbo.sp_create_evaluation_group {info.NombreEvaluacion}, {info.Rubro}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
                ");
                await _context.SaveChangesAsync();

                //Obtener evaluacion grupo recien creada

                var EvaluacionGrupo = (await _context.EvaluacionGrupo.FromSqlInterpolated($@"
                    dbo.sp_get_inserted_grupo
                ").ToListAsync()).FirstOrDefault(); 

                Console.WriteLine(EvaluacionGrupo.Id);

                var idEvaluacionGrupo = EvaluacionGrupo.Id; // obtener id de la evaluacion recien creada
                //Crear la evaluacion integrantes
                foreach (var estudiante in info.Estudiantes)
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        dbo.sp_create_evaluation_student {idEvaluacionGrupo}, {estudiante}
                    ");
                    await _context.SaveChangesAsync();

                }


            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex);
            }

            return Ok();


        }



    }
}