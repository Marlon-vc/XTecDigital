using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class GrupoEstudiante
    {
        public int NumeroGrupo { get; set; }
        public string Estudiante { get; set; }

        public virtual Grupo NumeroGrupoNavigation { get; set; }
    }
}
