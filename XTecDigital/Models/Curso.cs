using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Curso
    {
        public Curso()
        {
            CursoSemestre = new HashSet<CursoSemestre>();
            Grupo = new HashSet<Grupo>();
        }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Carrera { get; set; }
        public bool Habilitado { get; set; }

        public virtual ICollection<CursoSemestre> CursoSemestre { get; set; }
        public virtual ICollection<Grupo> Grupo { get; set; }
    }
}
