using System;

namespace XTecDigital.Models.Dtos
{
    public class ArchivoDto
    {
        public int Id { get; set; }
        public int IdCarpeta { get; set; }
        public string Nombre { get; set; }
        public decimal Tamanio { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}