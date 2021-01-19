using System.Collections.Generic;
using XTecDigital.Models.Mongo;

namespace XTecDigital.Models.Requests
{
    public class GrupoInfo
    {
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
        public List<string> Estudiantes { get; set; }
        public List<string> Profesores { get; set; }
    }
}