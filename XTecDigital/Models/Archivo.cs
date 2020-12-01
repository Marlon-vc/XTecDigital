using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Archivo
    {
        public Archivo()
        {
            EntregableArchivo = new HashSet<EntregableArchivo>();
            EntregableArchivoDetalle = new HashSet<EntregableArchivoDetalle>();
            EvaluacionEspecificacion = new HashSet<EvaluacionEspecificacion>();
        }

        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Nombre { get; set; }
        public decimal Tamanio { get; set; }
        public int IdCarpeta { get; set; }

        public virtual Carpeta IdCarpetaNavigation { get; set; }
        public virtual ICollection<EntregableArchivo> EntregableArchivo { get; set; }
        public virtual ICollection<EntregableArchivoDetalle> EntregableArchivoDetalle { get; set; }
        public virtual ICollection<EvaluacionEspecificacion> EvaluacionEspecificacion { get; set; }
    }
}
