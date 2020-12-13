using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class ProfesorGrupo
    {
        public string Profesor { get; set; }
        public int IdGrupo { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; }
    }
}
