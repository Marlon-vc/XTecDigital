using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class CursoSemestre
    {
        public int Id { get; set; }
        public string IdCurso { get; set; }
        public int IdSemestre { get; set; }

        public virtual Curso IdCursoNavigation { get; set; }
        public virtual Semestre IdSemestreNavigation { get; set; }
    }
}
