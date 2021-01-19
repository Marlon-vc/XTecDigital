using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Carpeta
    {
        public Carpeta()
        {
            Archivo = new HashSet<Archivo>();
        }

        public string Nombre { get; set; }
        public bool SoloLectura { get; set; }
        public string Tipo { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }

        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<Archivo> Archivo { get; set; }
    }
}
