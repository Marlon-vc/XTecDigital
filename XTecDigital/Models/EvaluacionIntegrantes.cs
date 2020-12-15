using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class EvaluacionIntegrantes
    {
        public string Estudiante { get; set; }
        public int IdGrupo { get; set; }

        public virtual EvaluacionGrupo IdGrupoNavigation { get; set; }
    }
}
