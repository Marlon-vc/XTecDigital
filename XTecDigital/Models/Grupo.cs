using System;
using System.Collections.Generic;

namespace XTecDigital.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            Carpeta = new HashSet<Carpeta>();
            EstudianteGrupo = new HashSet<EstudianteGrupo>();
            Noticia = new HashSet<Noticia>();
            ProfesorGrupo = new HashSet<ProfesorGrupo>();
            Rubro = new HashSet<Rubro>();
        }

        public int Id { get; set; }
        public int Numero { get; set; }
        public int IdCurso { get; set; }
        public int IdSemestre { get; set; }

        public virtual Curso IdCursoNavigation { get; set; }
        public virtual Semestre IdSemestreNavigation { get; set; }
        public virtual ICollection<Carpeta> Carpeta { get; set; }
        public virtual ICollection<EstudianteGrupo> EstudianteGrupo { get; set; }
        public virtual ICollection<Noticia> Noticia { get; set; }
        public virtual ICollection<ProfesorGrupo> ProfesorGrupo { get; set; }
        public virtual ICollection<Rubro> Rubro { get; set; }
    }
}
