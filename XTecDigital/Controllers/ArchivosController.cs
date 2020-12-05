using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTecDigital.Models;

namespace XTecDigital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivosController: ControllerBase
    {
        public readonly AppDbContext _context;
        public readonly IMapper _mapper;

        public ArchivosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{idGrupo}")]
        public async Task<IActionResult> GetArchivosAsync(int idGrupo)
        {
            var archivos = _context.Archivo.FromSqlInterpolated($@"
                [dbo].[sp_get_files] {idGrupo};
            ");

            return Ok(archivos);
        }
    }
}