using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Models;

namespace XTecDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public NoticiasController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/Noticias/Grupo/1
        [HttpGet("Grupo/{idGrupo}")]
        public IActionResult GetNoticiasGrupo(string idGrupo)
        {
            var result = _context.Noticia.FromSqlInterpolated($@"
                dbo.sp_get_noticias_grupo {idGrupo}
            ");

            return Ok(result);
        }

        //GET: api/Rubros/1
        [HttpGet("{id}")]
        public IActionResult GetNoticia(int idNoticia)
        {
            var noticia = _context.Noticia.FromSqlInterpolated($@"
                dbo.sp_get_noticia {idNoticia}
            ").AsEnumerable().FirstOrDefault();

            if (noticia == null)
                return NotFound();

            return Ok(noticia);
        }

        // POST: api/Noticias
        [HttpPost]
        public async Task<IActionResult> AddNoticiaAsync(Noticia noticia)
        {
            if (noticia == null)
                return BadRequest();
            
            if (NoticiaExists(noticia.Id))
                return Conflict();
            
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_noticia 
            ");

            await _context.SaveChangesAsync();

            return CreatedAtRoute("Default", new { noticia.Id }, noticia);
        }

        // PUT: api/Noticias/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRubroAsync(int id, Noticia noticia)
        {
            if (id != noticia.Id)
                return BadRequest();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_update_noticia
            ");

            return NoContent();
        }

        // DELETE: api/Noticias/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoticiaAsync(int id)
        {
            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_delete_noticia {id}
            ");

            if (rows == 0)
                return NotFound();
            
            return Ok();
        }

        private bool NoticiaExists(int id)
        {
            var noticia = _context.Noticia.FromSqlInterpolated($@"
                dbo.sp_get_noticia {id}
            ").AsEnumerable();

            return noticia.Any();
        }
    }
}