using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Archivo
    {
        public int Id { get; set; }
        public int IdCarpeta { get; set; }
        public string Nombre { get; set; }
        public decimal Tamanio { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual Carpeta IdCarpetaNavigation { get; set; }
    }
}
