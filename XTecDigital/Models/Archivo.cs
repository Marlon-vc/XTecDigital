using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Archivo
    {
        public Archivo()
        {
            Evaluacion = new HashSet<Evaluacion>();
            EvaluacionGrupoArchivo = new HashSet<EvaluacionGrupo>();
            EvaluacionGrupoArchivoNavigation = new HashSet<EvaluacionGrupo>();
        }

        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Tamanio { get; set; }
        public string Carpeta { get; set; }
        public string TipoCarpeta { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }

        public virtual Carpeta CarpetaNavigation { get; set; }
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
        public virtual ICollection<EvaluacionGrupo> EvaluacionGrupoArchivo { get; set; }
        public virtual ICollection<EvaluacionGrupo> EvaluacionGrupoArchivoNavigation { get; set; }
    }
}
