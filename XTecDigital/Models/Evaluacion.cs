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

        public string Nombre { get; set; }
        public bool NotasPublicadas { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal PesoNota { get; set; }
        public bool Grupal { get; set; }
        public string Especificacion { get; set; }
        public string CarpetaEspecificacion { get; set; }
        public string TipoCarpetaEspecificacion { get; set; }
        public string Rubro { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }

        public virtual Archivo Archivo { get; set; }
        public virtual Rubro RubroNavigation { get; set; }
        public virtual ICollection<EvaluacionGrupo> EvaluacionGrupo { get; set; }
    }
}
