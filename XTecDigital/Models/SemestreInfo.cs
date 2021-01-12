using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class SemestreInfo
    {
        public int IdSemestre { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
        public int IdGrupo { get; set; }
        public int NumeroGrupo { get; set; }
        public string IdCurso { get; set; }
        public string Nombre { get; set; }
        public int Creditos { get; set; }
        public string Carrera { get; set; }
        public string Profesor { get; set; }
        public string Estudiante { get; set; }
    }
}
