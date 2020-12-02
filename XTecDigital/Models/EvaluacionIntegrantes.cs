using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class EvaluacionIntegrantes
    {
        public int IdEvaluacion { get; set; }
        public int Estudiante { get; set; }

        public virtual Evaluacion IdEvaluacionNavigation { get; set; }
    }
}
