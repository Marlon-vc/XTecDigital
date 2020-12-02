using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class GrupoProfesores
    {
        public int NumeroGrupo { get; set; }
        public int Profesor { get; set; }

        public virtual Grupo NumeroGrupoNavigation { get; set; }
    }
}
