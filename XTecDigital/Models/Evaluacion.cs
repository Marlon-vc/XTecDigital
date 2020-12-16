using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Evaluacion
    {
        public Evaluacion()
        {
            EvaluacionGrupo = new HashSet<EvaluacionGrupo>();
        }

        public int Id { get; set; }
        public int IdRubro { get; set; }
        public int IdEspecificacion { get; set; }
        public string Nombre { get; set; }
        public bool NotasPublicadas { get; set; }
        public decimal PesoNota { get; set; }
        public bool Grupal { get; set; }
        public DateTime? FechaEntrega { get; set; }

        public virtual ArchivoEvaluacion IdEspecificacionNavigation { get; set; }
        public virtual Rubro IdRubroNavigation { get; set; }
        public virtual ICollection<EvaluacionGrupo> EvaluacionGrupo { get; set; }
    }
}
