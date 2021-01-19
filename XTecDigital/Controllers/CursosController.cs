using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using XTecDigital.Models;
using XTecDigital.Models.Dtos;

namespace XTecDigital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        
        public CursosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCursosAsync()
        {
            var result = await _context.Curso.FromSqlInterpolated($@"
                dbo.sp_get_courses
            ").ToListAsync();

            return Ok(_mapper.Map<List<CursoDto>>(result));
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetCursosActivosAsync()
        {
            var result = await _context.Curso.FromSqlInterpolated($@"
                dbo.sp_get_active_courses
            ").ToListAsync();

            return Ok(_mapper.Map<List<CursoDto>>(result));
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetCursoAsync(string codigo)
        {
            var curso = (await _context.Curso.FromSqlInterpolated($@"
                dbo.sp_get_course {codigo}
            ").ToListAsync()).FirstOrDefault();

            if (curso == null)
                return NotFound();

            return Ok(curso);
        }

        [HttpPost]
        public async Task<IActionResult> AddCursoAsync(Curso curso)
        {
            if (curso == null || string.IsNullOrWhiteSpace(curso.Codigo))
                return BadRequest();

            if (CursoExists(curso.Codigo))
                return Conflict();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_course {curso.Codigo}, {curso.Nombre}, {curso.Creditos}, {curso.Carrera}, {curso.Habilitado};
            ");
            await _context.SaveChangesAsync();

            return CreatedAtRoute("Default", new { codigo = curso.Codigo }, curso);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> UpdateCursoAsync(string codigo, Curso curso)
        {
            if (codigo != curso.Codigo)
                return BadRequest();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_update_course {codigo}, {curso.Nombre}, {curso.Carrera}, {curso.Creditos}, {curso.Habilitado};
            ");

            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteCursoAsync(string codigo)
        {
            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_delete_course {codigo}
            ");

            if (rows == 0)
                return NotFound();

            return Ok();
        }

        private bool CursoExists(string codigo)
        {
            return _context.Curso.FromSqlInterpolated($@"
                dbo.sp_get_course {codigo}
            ").ToList().Any();
        }
    }
}