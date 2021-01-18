using System;
using System.Collections.Generic;

namespace XTecDigital.Models.Requests
{
    public class AsignacionInfo
    {
        public string NombreEvaluacion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal PesoNota { get; set; }
        public bool Grupal { get; set; }
        public string NombreEspec { get; set; }
        public string Rubro { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
        public string FileData { get; set; }
        public int Size { get; set; }

        public List<String> Estudiantes { get; set; } 
    }
}