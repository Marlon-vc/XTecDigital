using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class InfoEvaluarEntregables
    {
        public string Estudiante { get; set; }
        public string Entregable { get; set; }
        public string CarpetaEntregable { get; set; }
        public string TipoCarpetaEntregable { get; set; }
        public string Detalle { get; set; }
        public string CarpetaDetalle { get; set; }
        public string TipoCarpetaDetalle { get; set; }
        public string Observaciones { get; set; }
        public decimal? Nota { get; set; }
        public string NombreEvaluacion { get; set; }
        public string Rubro { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
        public int? IdEvaluacionGrupo { get; set; }
        public string Profesor { get; set; }
    }
}
