using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class GrupoEstudiantes
    {
        public int NumeroGrupo { get; set; }
        public int Estudiante { get; set; }

        public virtual Grupo NumeroGrupoNavigation { get; set; }
    }
}
