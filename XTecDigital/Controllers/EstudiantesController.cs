using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XTecDigital.Helpers;
using XTecDigital.Models;
using XTecDigital.Models.Mongo;

namespace XTecDigital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _client;

        public EstudiantesController(AppDbContext context)
        {
            _context = context;
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetEstudiantes()
        {
            var response = await _client.GetStringAsync($"{Constants.MongoApi}/Estudiantes");
            var estudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(response);

            return Ok(estudiantes);
        }

        [HttpGet("{carnet}")]
        public async Task<IActionResult> GetEstudiante(string carnet) 
        {
            var response = await _client.GetAsync($"{Constants.MongoApi}/Estudiantes/{carnet}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var estudiante = JsonConvert.DeserializeObject<Estudiante>(await response.Content.ReadAsStringAsync());

            return Ok(estudiante);
            
        }
        
    }
}