using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class EvaluacionIntegrantes
    {
        public string Estudiante { get; set; }
        public int IdEvaluacion { get; set; }

        public virtual Evaluacion IdEvaluacionNavigation { get; set; }
    }
}
