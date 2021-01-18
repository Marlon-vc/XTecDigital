using System;

namespace XTecDigital.Models.Dtos
{
    public class NoticiaDto
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Autor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
    }
}