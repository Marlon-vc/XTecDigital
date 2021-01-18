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
    public class RubrosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RubrosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET: api/Rubros/Grupo
        [HttpGet("Grupo")]
        public async Task<IActionResult> GetRubrosGrupoAsync([FromQuery] GrupoDto grupo)
        {
            var result = await _context.Rubro.FromSqlInterpolated($@"
                dbo.sp_get_rubros_grupo {grupo.Numero}, {grupo.Curso}, {grupo.Anio}, {grupo.Periodo}
            ").ToListAsync();

            return Ok(_mapper.Map<List<RubroDto>>(result));
        }

        //GET: api/Rubros
        [HttpGet("Rubro")]
        public async Task<IActionResult> GetRubroAsync([FromBody] RubroRequest rubro)
        {
            var result = (await _context.Rubro.FromSqlInterpolated($@"
                dbo.sp_get_rubro {rubro.Nombre}, {rubro.Numero}, {rubro.Curso}, {rubro.Anio}, {rubro.Periodo}
            ").ToListAsync()).FirstOrDefault();

            if (result == null) 
                return NotFound();

            return Ok(_mapper.Map<RubroDto>(result));
        }

        // POST: api/Rubros
        [HttpPost]
        public async Task<IActionResult> AddRubroAsync(RubroDto rubro) 
        {
            if (rubro == null)
                return BadRequest();

            if (RubroExists(rubro))
                return Conflict();

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_create_rubro {rubro.Nombre}, {rubro.Porcentaje}, {rubro.Numero}, {rubro.Curso}, {rubro.Anio}, {rubro.Periodo}
            ");
            await _context.SaveChangesAsync();

            return CreatedAtRoute("Default", rubro);
        }

        // PUT: api/Rubros/1
        [HttpPut]
        public async Task<IActionResult> UpdateRubroAsync(RubroUpdate rubro) 
        {
            if (rubro == null)
                return BadRequest();
            
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                dbo.sp_update_rubro {rubro.Nombre}, {rubro.NuevoNombre}, {rubro.Porcentaje}, {rubro.Numero}, {rubro.Curso}, {rubro.Anio}, {rubro.Periodo}
            ");

            return NoContent();
        }

        // DELETE: api/Rubros/1
        [HttpDelete]
        public async Task<IActionResult> DeleteRubroAsync([FromQuery] RubroRequest rubro) 
        {
            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXECUTE dbo.sp_delete_rubro {rubro.Nombre}, {rubro.Numero}, {rubro.Curso}, {rubro.Anio}, {rubro.Periodo}
            ");

            if (rows == 0)
                return NotFound();

            return NoContent();
        }

        private bool RubroExists(RubroDto rubro) 
        {
            return _context.Rubro.FromSqlInterpolated($@"
                EXECUTE dbo.sp_get_rubro {rubro.Nombre}, {rubro.Numero}, {rubro.Curso}, {rubro.Anio}, {rubro.Periodo}
            ").ToList().Any();
        }
   
    }
}