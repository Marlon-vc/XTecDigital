using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Models;

namespace XTecDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeriodosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Periodos
        [HttpGet]
        public IActionResult GetPeriodos()
        {
            var result = _context.Periodo.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_periods;
            ");
            return Ok(result);
        }

        // GET: api/Periodos/5
        [HttpGet("{id}")]
        public IActionResult GetPeriodo(int id)
        {
            var periodo = _context.Periodo.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_period {id};
            ").AsEnumerable().FirstOrDefault();

            if (periodo == null)
            {
                return NotFound();
            }

            return Ok(periodo);
        }
    }
}
