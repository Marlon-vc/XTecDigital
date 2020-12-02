using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class EvaluacionEspecificacion
    {
        public int Id { get; set; }
        public int IdEvaluacion { get; set; }
        public int IdEspecificacion { get; set; }

        public virtual Archivo IdEspecificacionNavigation { get; set; }
        public virtual Evaluacion IdEvaluacionNavigation { get; set; }
    }
}
