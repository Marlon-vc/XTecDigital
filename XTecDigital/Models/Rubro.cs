using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Rubro
    {
        public Rubro()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        public string Nombre { get; set; }
        public decimal Porcentaje { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }

        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
