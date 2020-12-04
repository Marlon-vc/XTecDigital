using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XTecDigital.Helpers;
using XTecDigital.Models.Mongo;

namespace XTecDigital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController: ControllerBase
    {
        private readonly HttpClient _client;

        public ProfesoresController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetProfesors()
        {
            var response = await _client.GetStringAsync($"{Constants.MongoApi}/Profesores");
            var profesores = JsonConvert.DeserializeObject<List<Profesor>>(response);

            return Ok(profesores);
        }

        [HttpGet("{cedula}")]
        public async Task<IActionResult> GetProfesor(string cedula) 
        {
            var response = await _client.GetAsync($"{Constants.MongoApi}/Profesores/{cedula}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var profesor = JsonConvert.DeserializeObject<Profesor>(await response.Content.ReadAsStringAsync());

            return Ok(profesor);
            
        }
        
    }
}