using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class EvaluacionGrupo
    {
        public EvaluacionGrupo()
        {
            EvaluacionIntegrantes = new HashSet<EvaluacionIntegrantes>();
        }

        public int Id { get; set; }
        public int IdEvaluacion { get; set; }
        public string Observaciones { get; set; }
        public int? IdEntregable { get; set; }
        public int? IdDetalle { get; set; }

        public virtual ArchivoEvaluacion IdDetalleNavigation { get; set; }
        public virtual ArchivoEvaluacion IdEntregableNavigation { get; set; }
        public virtual Evaluacion IdEvaluacionNavigation { get; set; }
        public virtual ICollection<EvaluacionIntegrantes> EvaluacionIntegrantes { get; set; }
    }
}
