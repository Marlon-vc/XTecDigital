using System;

namespace XTecDigital.Models.Requests
{
    public class NoticiaRequest
    {
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
    }
}