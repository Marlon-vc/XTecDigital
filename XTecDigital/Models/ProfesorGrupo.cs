using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class ProfesorGrupo
    {
        public string Profesor { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }

        public virtual Grupo Grupo { get; set; }
    }
}
