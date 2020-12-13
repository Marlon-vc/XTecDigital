using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Entregable
    {
        public string Estudiante { get; set; }
        public string Observaciones { get; set; }
        public int IdEvaluacion { get; set; }
        public int IdEntregable { get; set; }
        public int IdDetalle { get; set; }

        public virtual Evaluacion IdEvaluacionNavigation { get; set; }
    }
}
