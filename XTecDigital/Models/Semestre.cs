using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Semestre
    {
        public Semestre()
        {
            CursoSemestre = new HashSet<CursoSemestre>();
        }

        public int Id { get; set; }
        public int Anio { get; set; }
        public int IdPeriodo { get; set; }

        public virtual Periodo IdPeriodoNavigation { get; set; }
        public virtual ICollection<CursoSemestre> CursoSemestre { get; set; }
    }
}
