using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Entregable
    {
        public Entregable()
        {
            EntregableArchivo = new HashSet<EntregableArchivo>();
            EntregableArchivoDetalle = new HashSet<EntregableArchivoDetalle>();
        }

        public int Id { get; set; }
        public string Observaciones { get; set; }
        public int Estudiante { get; set; }
        public int IdEvaluacion { get; set; }

        public virtual Evaluacion IdEvaluacionNavigation { get; set; }
        public virtual ICollection<EntregableArchivo> EntregableArchivo { get; set; }
        public virtual ICollection<EntregableArchivoDetalle> EntregableArchivoDetalle { get; set; }
    }
}
