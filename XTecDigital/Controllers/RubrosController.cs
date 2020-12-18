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

        //GET: api/Rubros/Grupo/1
        [HttpGet("Grupo/{idGrupo}")]
        public IActionResult GetRubrosGrupo(string idGrupo)
        {
            var result = _context.Rubro.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_rubros_grupo {idGrupo}
            ");

            return Ok(result);
        }

        //GET: api/Rubros/1
        [HttpGet("{id}")]
        public IActionResult GetRubro(int id)
        {
            var rubro = _context.Rubro.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_rubro {id}
            ").AsEnumerable().FirstOrDefault();

            if (rubro == null) 
                return NotFound();

            return Ok(rubro);
        }

        // POST: api/Rubros
        [HttpPost]
        public async Task<IActionResult> AddRubroAsync(Rubro rubro) 
        {
            if (rubro == null)
                return BadRequest();

            if (RubroExists(rubro.Id))
                return Conflict();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXECUTE dbo.sp_create_rubro {rubro.Nombre}, {rubro.Porcentaje}, {rubro.IdGrupo}
            ");
            
            await _context.SaveChangesAsync();

            return CreatedAtRoute("Default", new { rubro.Id }, rubro);
        }

        // PUT: api/Rubros/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRubroAsync(int id, Rubro rubro) 
        {
            if (id != rubro.Id)
                return BadRequest();
            
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXECUTE dbo.sp_update_rubro {rubro.Nombre}, {rubro.Id}, {rubro.Porcentaje}
            ");

            return NoContent();
        }

        // DELETE: api/Rubros/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRubroAsync(int id) 
        {
            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXECUTE dbo.sp_delete_rubro {id};
            ");

            if (rows == 0)
                return NotFound();

            return Ok();
        }

        private bool RubroExists(int id) 
        {
            var rubro = _context.Rubro.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_rubro {id}
            ").AsEnumerable();
            return rubro.Any();
        }

        
    }
}