using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Models;

namespace XTecDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EvaluacionesController(AppDbContext context) 
        {
            _context = context;
        }

        //GET: api/Evaluaciones/Rubro/idRubro
        [HttpGet("Rubro/{idRubro}")]
        public async Task<IActionResult> GetEvaluacionesRubroAsync(int idRubro) 
        {
            var result = await _context.Evaluacion.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_evaluaciones_rubro {idRubro}
            ").ToListAsync();

            return Ok(result);
        }

        //GET: api/Evaluaciones/Info/{idEvaluacion}
        [HttpGet("Info/{idEvaluacion}")]
        public async Task<IActionResult> GetInfoEvaluacionAsync(int idEvaluacion)
        {
            var result = (await _context.InfoEvaluacion.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_info_evaluacion {idEvaluacion}
            ").ToListAsync()).FirstOrDefault();

            return Ok(result);
        }
               
    }
}