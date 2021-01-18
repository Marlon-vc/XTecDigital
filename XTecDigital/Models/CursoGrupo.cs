using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class CursoGrupo
    {
        public string Codigo { get; set; }
        public string NombreCurso { get; set; }
        public int? Creditos { get; set; }
        public string Carrera { get; set; }
        public int NumeroGrupo { get; set; }
        public int AnioSemestre { get; set; }
        public string PeriodoSemestre { get; set; }
    }
}
