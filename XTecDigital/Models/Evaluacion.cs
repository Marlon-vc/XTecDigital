using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Evaluacion
    {
        public Evaluacion()
        {
            Entregable = new HashSet<Entregable>();
            EvaluacionEspecificacion = new HashSet<EvaluacionEspecificacion>();
            EvaluacionIntegrantes = new HashSet<EvaluacionIntegrantes>();
        }

        public int Id { get; set; }
        public bool EsIndividual { get; set; }
        public bool NotasPublicadas { get; set; }
        public int Peso { get; set; }
        public DateTime FechaEntrega { get; set; }
        public TimeSpan HoraEntrega { get; set; }
        public string IdRubro { get; set; }

        public virtual Rubro IdRubroNavigation { get; set; }
        public virtual ICollection<Entregable> Entregable { get; set; }
        public virtual ICollection<EvaluacionEspecificacion> EvaluacionEspecificacion { get; set; }
        public virtual ICollection<EvaluacionIntegrantes> EvaluacionIntegrantes { get; set; }
    }
}
