using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class InfoEvaluacion
    {
        public int IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public decimal PesoNota { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string ArchivoEspecificacion { get; set; }
        public DateTime? FechaCreacionEspec { get; set; }
        public int IdEspec { get; set; }
        public string ArchivoEntregable { get; set; }
        public DateTime? FechaCreacionEntre { get; set; }
        public int IdEntre { get; set; }
        public int IdGrupo { get; set; }
        public decimal? NotaFinal { get; set; }
        public string Observaciones { get; set; }
        public string Retroalimentacion { get; set; }
        public int IdRetro { get; set; }
    }
}
