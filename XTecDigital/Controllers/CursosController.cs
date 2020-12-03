using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using XTecDigital.Models;

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
        public IActionResult GetCursos()
        {
            var result = _context.Curso.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_courses;
            ");

            return Ok(result);
        }

        [HttpGet("active")]
        public IActionResult GetCursosActivos()
        {
            var result = _context.Curso.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_active_courses;
            ");

            return Ok(result);
        }

        [HttpGet("{codigo}")]
        public IActionResult GetCurso(string codigo)
        {
            var curso = _context.Curso.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_course {codigo};
            ").AsEnumerable().FirstOrDefault();

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
                EXECUTE dbo.sp_create_course 
                {curso.Codigo}, {curso.Nombre}, {curso.Carrera}, {curso.Habilitado};
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
                EXECUTE dbo.sp_update_course {codigo}, {curso.Nombre}, {curso.Carrera}, {curso.Habilitado};
            ");

            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteCursoAsync(string codigo)
        {
            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXECUTE dbo.sp_delete_course {codigo};
            ");

            if (rows == 0)
                return NotFound();

            return Ok();
        }

        private bool CursoExists(string codigo)
        {
            var results = _context.Curso.FromSqlInterpolated($"EXECUTE dbo.sp_get_course {codigo};").AsEnumerable(); 
            return results.Any();
        }
    }
}