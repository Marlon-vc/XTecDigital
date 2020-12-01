using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Periodo
    {
        public Periodo()
        {
            Semestre = new HashSet<Semestre>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Semestre> Semestre { get; set; }
    }
}
