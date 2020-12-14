using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Semestre
    {
        public Semestre()
        {
            Grupo = new HashSet<Grupo>();
        }

        public int Id { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }

        public virtual ICollection<Grupo> Grupo { get; set; }
    }
}
