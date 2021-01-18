using System;

namespace XTecDigital.Models.Dtos
{
    public class EvaluacionDto
    {
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
    }
}