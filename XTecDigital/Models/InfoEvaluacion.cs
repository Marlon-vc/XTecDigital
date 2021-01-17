using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class InfoEvaluacion
    {
        public string Nombre { get; set; }
        public bool NotasPublicadas { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal PesoNota { get; set; }
        public bool Grupal { get; set; }
        public string Rubro { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
        public string Especificacion { get; set; }
        public string CarpetaEspecificacion { get; set; }
        public string TipoCarpetaEspecificacion { get; set; }
        public string Entregable { get; set; }
        public string CarpetaEntregable { get; set; }
        public string TipoCarpetaEntregable { get; set; }
        public DateTime? FechaEntregable { get; set; }
        public string Detalle { get; set; }
        public string CarpetaDetalle { get; set; }
        public string TipoCarpetaDetalle { get; set; }
        public int? IdEvaluacionGrupo { get; set; }
        public decimal? Nota { get; set; }
        public string Observaciones { get; set; }
    }
}
