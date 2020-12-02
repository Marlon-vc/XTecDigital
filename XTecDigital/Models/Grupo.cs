using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            Carpeta = new HashSet<Carpeta>();
            GrupoEstudiantes = new HashSet<GrupoEstudiantes>();
            GrupoProfesores = new HashSet<GrupoProfesores>();
            Noticia = new HashSet<Noticia>();
            Rubro = new HashSet<Rubro>();
        }

        public int Numero { get; set; }
        public string IdCurso { get; set; }

        public virtual Curso IdCursoNavigation { get; set; }
        public virtual ICollection<Carpeta> Carpeta { get; set; }
        public virtual ICollection<GrupoEstudiantes> GrupoEstudiantes { get; set; }
        public virtual ICollection<GrupoProfesores> GrupoProfesores { get; set; }
        public virtual ICollection<Noticia> Noticia { get; set; }
        public virtual ICollection<Rubro> Rubro { get; set; }
    }
}
