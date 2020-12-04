using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class GrupoProfesor
    {
        public int NumeroGrupo { get; set; }
        public string Profesor { get; set; }

        public virtual Grupo NumeroGrupoNavigation { get; set; }
    }
}
