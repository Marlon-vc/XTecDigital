using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            Carpeta = new HashSet<Carpeta>();
            GrupoEstudiante = new HashSet<GrupoEstudiante>();
            GrupoProfesor = new HashSet<GrupoProfesor>();
            Noticia = new HashSet<Noticia>();
            Rubro = new HashSet<Rubro>();
        }

        public int Id { get; set; }
        public int Numero { get; set; }
        public string IdCurso { get; set; }

        public virtual Curso IdCursoNavigation { get; set; }
        public virtual ICollection<Carpeta> Carpeta { get; set; }
        public virtual ICollection<GrupoEstudiante> GrupoEstudiante { get; set; }
        public virtual ICollection<GrupoProfesor> GrupoProfesor { get; set; }
        public virtual ICollection<Noticia> Noticia { get; set; }
        public virtual ICollection<Rubro> Rubro { get; set; }
    }
}
