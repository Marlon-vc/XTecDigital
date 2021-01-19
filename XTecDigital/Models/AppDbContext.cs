using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace XTecDigital.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Archivo> Archivo { get; set; }
        public virtual DbSet<Carpeta> Carpeta { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<CursoGrupo> CursoGrupo { get; set; }
        public virtual DbSet<EstudianteGrupo> EstudianteGrupo { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<EvaluacionGrupo> EvaluacionGrupo { get; set; }
        public virtual DbSet<EvaluacionIntegrantes> EvaluacionIntegrantes { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<InfoEvaluacion> InfoEvaluacion { get; set; }
        public virtual DbSet<InfoEvaluarEntregables> InfoEvaluarEntregables { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<ProfesorGrupo> ProfesorGrupo { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<Semestre> Semestre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:xtecdigitalcr.database.windows.net,1433;Initial Catalog=xtecdigital2;Persist Security Info=False;User ID=xtec_admin;Password=ONCEdeENERO-99;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Carpeta, e.TipoCarpeta, e.Numero, e.Curso, e.Anio, e.Periodo })
                    .HasName("PK__ARCHIVO__0C346F4B82A9F2BB");

                entity.ToTable("ARCHIVO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Carpeta)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCarpeta)
                    .HasColumnName("Tipo_carpeta")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("Fecha_creacion")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CarpetaNavigation)
                    .WithMany(p => p.Archivo)
                    .HasForeignKey(d => new { d.Carpeta, d.TipoCarpeta, d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__ARCHIVO__7B5B524B");
            });

            modelBuilder.Entity<Carpeta>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Tipo, e.Numero, e.Curso, e.Anio, e.Periodo })
                    .HasName("PK__CARPETA__8AF656455111F09C");

                entity.ToTable("CARPETA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SoloLectura).HasColumnName("Solo_lectura");

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.Carpeta)
                    .HasForeignKey(d => new { d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__CARPETA__7A672E12");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__CURSO__06370DAD1DD04DA2");

                entity.ToTable("CURSO");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Carrera)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CursoGrupo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CURSO_GRUPO");

                entity.Property(e => e.AnioSemestre).HasColumnName("Anio_semestre");

                entity.Property(e => e.Carrera)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCurso)
                    .HasColumnName("Nombre_curso")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroGrupo).HasColumnName("Numero_grupo");

                entity.Property(e => e.PeriodoSemestre)
                    .IsRequired()
                    .HasColumnName("Periodo_semestre")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<EstudianteGrupo>(entity =>
            {
                entity.HasKey(e => new { e.Estudiante, e.Numero, e.Curso, e.Anio, e.Periodo })
                    .HasName("PK__ESTUDIAN__21A73E33F1ABA88F");

                entity.ToTable("ESTUDIANTE_GRUPO");

                entity.Property(e => e.Estudiante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.EstudianteGrupo)
                    .HasForeignKey(d => new { d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__ESTUDIANTE_GRUPO__74AE54BC");
            });

            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Rubro, e.Numero, e.Curso, e.Anio, e.Periodo })
                    .HasName("PK__EVALUACI__E802BACA974CEED4");

                entity.ToTable("EVALUACION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rubro)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CarpetaEspecificacion)
                    .IsRequired()
                    .HasColumnName("Carpeta_especificacion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Especificacion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEntrega)
                    .HasColumnName("Fecha_entrega")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotasPublicadas).HasColumnName("Notas_publicadas");

                entity.Property(e => e.PesoNota)
                    .HasColumnName("Peso_nota")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.TipoCarpetaEspecificacion)
                    .IsRequired()
                    .HasColumnName("Tipo_carpeta_especificacion")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.RubroNavigation)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => new { d.Rubro, d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__EVALUACION__778AC167");

                entity.HasOne(d => d.Archivo)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => new { d.Especificacion, d.CarpetaEspecificacion, d.TipoCarpetaEspecificacion, d.Numero, d.Curso, d.Anio, d.Periodo })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EVALUACION__7C4F7684");
            });

            modelBuilder.Entity<EvaluacionGrupo>(entity =>
            {
                entity.ToTable("EVALUACION_GRUPO");

                entity.Property(e => e.CarpetaDetalle)
                    .HasColumnName("Carpeta_detalle")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CarpetaEntregable)
                    .HasColumnName("Carpeta_entregable")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Detalle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Entregable)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Evaluacion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nota).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.Property(e => e.Periodo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Rubro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCarpetaDetalle)
                    .HasColumnName("Tipo_carpeta_detalle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCarpetaEntregable)
                    .HasColumnName("Tipo_carpeta_entregable")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.EvaluacionNavigation)
                    .WithMany(p => p.EvaluacionGrupo)
                    .HasForeignKey(d => new { d.Evaluacion, d.Rubro, d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__EVALUACION_GRUPO__787EE5A0");

                entity.HasOne(d => d.Archivo)
                    .WithMany(p => p.EvaluacionGrupoArchivo)
                    .HasForeignKey(d => new { d.Detalle, d.CarpetaDetalle, d.TipoCarpetaDetalle, d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__EVALUACION_GRUPO__7E37BEF6");

                entity.HasOne(d => d.ArchivoNavigation)
                    .WithMany(p => p.EvaluacionGrupoArchivoNavigation)
                    .HasForeignKey(d => new { d.Entregable, d.CarpetaEntregable, d.TipoCarpetaEntregable, d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__EVALUACION_GRUPO__7D439ABD");
            });

            modelBuilder.Entity<EvaluacionIntegrantes>(entity =>
            {
                entity.HasKey(e => new { e.Estudiante, e.IdGrupo })
                    .HasName("PK__EVALUACI__C6A208F2DB97EC44");

                entity.ToTable("EVALUACION_INTEGRANTES");

                entity.Property(e => e.Estudiante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.EvaluacionIntegrantes)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__EVALUACIO__Id_gr__797309D9");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => new { e.Numero, e.Curso, e.Anio, e.Periodo })
                    .HasName("PK__GRUPO__F2DB40B65F305F45");

                entity.ToTable("GRUPO");

                entity.Property(e => e.Curso)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CursoNavigation)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => d.Curso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GRUPO__Curso__72C60C4A");

                entity.HasOne(d => d.Semestre)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => new { d.Anio, d.Periodo })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GRUPO__71D1E811");
            });

            modelBuilder.Entity<InfoEvaluacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("INFO_EVALUACION");

                entity.Property(e => e.CarpetaDetalle)
                    .HasColumnName("Carpeta_detalle")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CarpetaEntregable)
                    .HasColumnName("Carpeta_entregable")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CarpetaEspecificacion)
                    .HasColumnName("Carpeta_especificacion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Detalle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Entregable)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Especificacion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEntrega)
                    .HasColumnName("Fecha_entrega")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaEntregable)
                    .HasColumnName("Fecha_entregable")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdEvaluacionGrupo).HasColumnName("Id_evaluacion_grupo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nota).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.NotasPublicadas).HasColumnName("Notas_publicadas");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.Property(e => e.Periodo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PesoNota)
                    .HasColumnName("Peso_nota")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Rubro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCarpetaDetalle)
                    .HasColumnName("Tipo_carpeta_detalle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCarpetaEntregable)
                    .HasColumnName("Tipo_carpeta_entregable")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCarpetaEspecificacion)
                    .HasColumnName("Tipo_carpeta_especificacion")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InfoEvaluarEntregables>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("INFO_EVALUAR_ENTREGABLES");

                entity.Property(e => e.CarpetaDetalle)
                    .HasColumnName("Carpeta_detalle")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CarpetaEntregable)
                    .HasColumnName("Carpeta_entregable")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Detalle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Entregable)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estudiante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdEvaluacionGrupo).HasColumnName("Id_evaluacion_grupo");

                entity.Property(e => e.NombreEvaluacion)
                    .IsRequired()
                    .HasColumnName("Nombre_evaluacion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nota).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.Property(e => e.Periodo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Profesor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rubro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCarpetaDetalle)
                    .HasColumnName("Tipo_carpeta_detalle")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCarpetaEntregable)
                    .HasColumnName("Tipo_carpeta_entregable")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.HasKey(e => new { e.Titulo, e.FechaPublicacion, e.Numero, e.Curso, e.Anio, e.Periodo })
                    .HasName("PK__NOTICIA__ED790B517399CAA2");

                entity.ToTable("NOTICIA");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPublicacion)
                    .HasColumnName("Fecha_publicacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.Curso)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Autor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => new { d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__NOTICIA__75A278F5");
            });

            modelBuilder.Entity<ProfesorGrupo>(entity =>
            {
                entity.HasKey(e => new { e.Profesor, e.Numero, e.Curso, e.Anio, e.Periodo })
                    .HasName("PK__PROFESOR__E56B5DFDF0AFCD6D");

                entity.ToTable("PROFESOR_GRUPO");

                entity.Property(e => e.Profesor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.ProfesorGrupo)
                    .HasForeignKey(d => new { d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__PROFESOR_GRUPO__73BA3083");
            });

            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.HasKey(e => new { e.Nombre, e.Numero, e.Curso, e.Anio, e.Periodo })
                    .HasName("PK__RUBRO__0ACE5BC5872C9670");

                entity.ToTable("RUBRO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Curso)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Porcentaje).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.Rubro)
                    .HasForeignKey(d => new { d.Numero, d.Curso, d.Anio, d.Periodo })
                    .HasConstraintName("FK__RUBRO__76969D2E");
            });

            modelBuilder.Entity<Semestre>(entity =>
            {
                entity.HasKey(e => new { e.Anio, e.Periodo })
                    .HasName("PK__SEMESTRE__3264416FE9B85481");

                entity.ToTable("SEMESTRE");

                entity.Property(e => e.Periodo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
