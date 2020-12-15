using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class ArchivoEvaluacion
    {
        public ArchivoEvaluacion()
        {
            Evaluacion = new HashSet<Evaluacion>();
            EvaluacionGrupoIdDetalleNavigation = new HashSet<EvaluacionGrupo>();
            EvaluacionGrupoIdEntregableNavigation = new HashSet<EvaluacionGrupo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
        public virtual ICollection<EvaluacionGrupo> EvaluacionGrupoIdDetalleNavigation { get; set; }
        public virtual ICollection<EvaluacionGrupo> EvaluacionGrupoIdEntregableNavigation { get; set; }
    }
}
