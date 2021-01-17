using System;

namespace XTecDigital.Models.Dtos
{
    public class ArchivoDto
    {
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Tamanio { get; set; }
        public string Carpeta { get; set; }
        public string TipoCarpeta { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
    }
}