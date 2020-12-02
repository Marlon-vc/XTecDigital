using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using XTecDigital.Helpers;
using XTecDigital.Models;
using XTecDigital.Models.Mongo;
using XTecDigital.Models.Requests;

namespace XTecDigital.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _client;

        public LoginController(AppDbContext context)
        {
            _context = context;
            _client = new HttpClient();
        }

        [HttpPost]
        public async Task<IActionResult> OnLoginAsync(LoginInfo info)
        {

            if (info == null || string.IsNullOrWhiteSpace(info.User) || string.IsNullOrWhiteSpace(info.Pass))
                return BadRequest();

            string[] types = { "Estudiantes", "Profesores", "Admin" };
            string currentUser = null, type = null;

            for (int i = 0; i < types.Length; i ++)
            {
                var response = await _client.GetAsync($"{Constants.MongoApi}/{types[i]}/{info.User}");
                if (!response.IsSuccessStatusCode)
                    continue;

                currentUser = await response.Content.ReadAsStringAsync();

                if (currentUser != null)
                {
                    type = types[i];
                    break;
                }
            }

            if (currentUser == null)
                return BadRequest();

            var data = JObject.Parse(currentUser);
            string user, userType;

            switch (type)
            {
                case "Estudiantes":
                    user = data["carnet"].ToString();
                    userType = "estudiante";
                    break;
                case "Profesores":
                    user = data["cedula"].ToString();
                    userType = "profesor";
                    break;
                case "Admin":
                    user = data["user"].ToString();
                    userType = "admin";
                    break;
                default:
                    return BadRequest();
            }

            var pass = data["pass"].ToString();

            if (!info.User.ToLower().Equals(user.ToLower()))
                return BadRequest();

            if (!Encryption.Matches(info.Pass, pass))
                return BadRequest();

            var userInfo = new
            {
                userId = user,
                type = userType
            };

            return Ok(userInfo);
        }


    }
}