using System;
using System.Collections.Generic;
using System.Linq;
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
    public class NoticiasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NoticiasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET: api/Noticias/Grupo/1
        [HttpGet("Grupo")]
        public async Task<IActionResult> GetNoticiasGrupoAsync(GrupoDto grupo)
        {
            var result = await _context.Noticia.FromSqlInterpolated($@"
                dbo.sp_get_noticias_grupo {grupo.Numero}, {grupo.Curso}, {grupo.Anio}, {grupo.Periodo}
            ").ToListAsync();

            return Ok(_mapper.Map<List<NoticiaDto>>(result));
        }

        //GET: api/Rubros/1
        [HttpGet]
        public async Task<IActionResult> GetNoticiaAsync(NoticiaRequest info)
        {
            var noticia = (await _context.Noticia.FromSqlInterpolated($@"
                dbo.sp_get_noticia {info.Titulo}, {info.FechaPublicacion}, {info.Numero}, {info.Curso}, {info.Anio}, {info.Periodo}
            ").ToListAsync()).FirstOrDefault();

            if (noticia == null)
                return NotFound();

            return Ok(_mapper.Map<NoticiaDto>(noticia));
        }

        // POST: api/Noticias
        [HttpPost]
        public async Task<IActionResult> AddNoticiaAsync(NoticiaDto noticia)
        {
            if (noticia == null)
                return BadRequest();
            
            if (NoticiaExists(noticia))
                return Conflict();
            
            noticia.FechaPublicacion = DateTime.Now;
            
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_noticia {noticia.Titulo}, {noticia.Mensaje}, {noticia.Autor}, {noticia.FechaPublicacion}, {noticia.Numero}, {noticia.Curso}, {noticia.Anio}, {noticia.Periodo}
            ");
            await _context.SaveChangesAsync();

            return CreatedAtRoute("Default", noticia);
        }

        // PUT: api/Noticias/1
        [HttpPut]
        public async Task<IActionResult> UpdateRubroAsync(NoticiaUpdate noticia)
        {
            if (noticia == null)
                return BadRequest();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_update_noticia {noticia.Titulo}, {noticia.NuevoTitulo}, {noticia.Mensaje}, {noticia.FechaPublicacion}, {noticia.Numero}, {noticia.Curso}, {noticia.Anio}, {noticia.Periodo}
            ");

            return NoContent();
        }

        // DELETE: api/Noticias/1
        [HttpDelete]
        public async Task<IActionResult> DeleteNoticiaAsync(NoticiaRequest noticia)
        {
            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_delete_noticia {noticia.Titulo}, {noticia.FechaPublicacion}, {noticia.Numero}, {noticia.Curso}, {noticia.Anio}, {noticia.Periodo}
            ");

            if (rows == 0)
                return NotFound();
            
            return Ok();
        }

        private bool NoticiaExists(NoticiaDto noticia)
        {
            return _context.Noticia.FromSqlInterpolated($@"
                dbo.sp_get_noticia {noticia.Titulo}, {noticia.FechaPublicacion}, {noticia.Numero}, {noticia.Curso}, {noticia.Anio}, {noticia.Periodo}
            ").ToList().Any();
        }
    }
}