using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Noticia
    {
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Autor { get; set; }
        public DateTime? FechaPublicacion { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; }
    }
}
