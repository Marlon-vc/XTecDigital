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

        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string Nombre { get; set; }
        public decimal Porcentaje { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; }
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
