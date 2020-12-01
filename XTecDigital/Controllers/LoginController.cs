using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XTecDigital.Models;

namespace XTecDigital.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController: ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> OnLoginAsync()
        {
            //TODO: implementar

            return Ok();
        }
    }
}