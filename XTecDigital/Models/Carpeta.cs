using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Carpeta
    {
        public Carpeta()
        {
            Archivo = new HashSet<Archivo>();
            CarpetaRaiz = new HashSet<CarpetaRaiz>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool SoloLectura { get; set; }
        public string Ruta { get; set; }
        public int IdGrupo { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; }
        public virtual ICollection<Archivo> Archivo { get; set; }
        public virtual ICollection<CarpetaRaiz> CarpetaRaiz { get; set; }
    }
}
