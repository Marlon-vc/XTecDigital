using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Archivo
    {
        public Archivo()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        public int Id { get; set; }
        public int IdCarpeta { get; set; }
        public string Nombre { get; set; }
        public byte[] FechaCreacion { get; set; }
        public decimal Tamanio { get; set; }
        public bool Visible { get; set; }

        public virtual Carpeta IdCarpetaNavigation { get; set; }
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
