using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Models;
using XTecDigital.Models.Dtos;
using XTecDigital.Models.Requests;

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
               
    }
}