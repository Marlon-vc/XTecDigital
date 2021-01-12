using Microsoft.AspNetCore.Http;

namespace XTecDigital.Models.Requests
{
    public class ArchivoCreacionInfo
    {
        public string IdGrupo { get; set; }
        public string IdCarpeta { get; set; }
        public IFormFile Archivo { get; set; }
    }
}