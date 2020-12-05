using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Models;
using XTecDigital.Models.Requests;

namespace XTecDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RubrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RubrosController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/Rubros/CE
        [HttpGet("{idCurso}")]
        public IActionResult GetRubrosGrupo(string idCurso)
        {
            return Ok();
        }

        // POST: api/Rubros
        [HttpPost]
        public async Task<IActionResult> AddRubroAsync(Rubro data) 
        {
            return Ok();
        }

        // PUT: api/Rubros/Examenes/CE
        [HttpPut("{nombre}/{idCurso}")]
        public async Task<IActionResult> UpdateRubroAsync(string nombre, string idCurso) 
        {
            return Ok();
        }

        // DELETE: api/Rubros/Examenes/CE
        [HttpDelete("{nombre}/{idCurso}")]
        public async Task<IActionResult> DeleteRubroAsync(string nombre, string idCurso) 
        {
            return Ok();
        }

        private bool RubroExists(string nombre, string idCurso) 
        {
            return true;
        }

        
    }
}