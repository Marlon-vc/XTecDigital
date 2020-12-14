using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Evaluacion
    {
        public Evaluacion()
        {
            Entregable = new HashSet<Entregable>();
            EvaluacionIntegrantes = new HashSet<EvaluacionIntegrantes>();
        }

        public int Id { get; set; }
        public int IdRubro { get; set; }
        public int IdEspecificacion { get; set; }
        public string Nombre { get; set; }
        public bool NotasPublicadas { get; set; }
        public byte[] FechaEntrega { get; set; }
        public decimal PesoNota { get; set; }
        public bool Grupal { get; set; }

        public virtual Archivo IdEspecificacionNavigation { get; set; }
        public virtual Rubro IdRubroNavigation { get; set; }
        public virtual ICollection<Entregable> Entregable { get; set; }
        public virtual ICollection<EvaluacionIntegrantes> EvaluacionIntegrantes { get; set; }
    }
}
