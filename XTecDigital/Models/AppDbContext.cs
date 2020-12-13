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
        public virtual DbSet<Entregable> Entregable { get; set; }
        public virtual DbSet<EstudianteGrupo> EstudianteGrupo { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<EvaluacionIntegrantes> EvaluacionIntegrantes { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<ProfesorGrupo> ProfesorGrupo { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<Semestre> Semestre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:xtecdigitalcr.database.windows.net,1433;Initial Catalog=xtecdigital;Persist Security Info=False;User ID=xtec_admin;Password=Tjg*%Ui9BM5K;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.ToTable("ARCHIVO");

                entity.HasIndex(e => new { e.IdCarpeta, e.Nombre })
                    .HasName("ARCHIVO_index_7")
                    .IsUnique();

                entity.Property(e => e.FechaCreacion)
                    .IsRequired()
                    .HasColumnName("Fecha_creacion")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.IdCarpeta).HasColumnName("Id_carpeta");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tamanio).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.IdCarpetaNavigation)
                    .WithMany(p => p.Archivo)
                    .HasForeignKey(d => d.IdCarpeta)
                    .HasConstraintName("FK__ARCHIVO__Id_carp__7A672E12");
            });

            modelBuilder.Entity<Carpeta>(entity =>
            {
                entity.ToTable("CARPETA");

                entity.HasIndex(e => new { e.IdGrupo, e.Nombre })
                    .HasName("CARPETA_index_6")
                    .IsUnique();

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ruta)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SoloLectura).HasColumnName("Solo_lectura");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Carpeta)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__CARPETA__Id_grup__797309D9");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.ToTable("CURSO");

                entity.HasIndex(e => e.Codigo)
                    .HasName("CURSO_index_1")
                    .IsUnique();

                entity.Property(e => e.Carrera)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Entregable>(entity =>
            {
                entity.HasKey(e => new { e.Estudiante, e.IdEvaluacion })
                    .HasName("PK__ENTREGAB__73BA28F612F4EC38");

                entity.ToTable("ENTREGABLE");

                entity.Property(e => e.Estudiante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdEvaluacion).HasColumnName("Id_evaluacion");

                entity.Property(e => e.IdDetalle).HasColumnName("Id_detalle");

                entity.Property(e => e.IdEntregable).HasColumnName("Id_entregable");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.HasOne(d => d.IdEvaluacionNavigation)
                    .WithMany(p => p.Entregable)
                    .HasForeignKey(d => d.IdEvaluacion)
                    .HasConstraintName("FK__ENTREGABL__Id_ev__7C4F7684");
            });

            modelBuilder.Entity<EstudianteGrupo>(entity =>
            {
                entity.HasKey(e => new { e.Estudiante, e.IdGrupo })
                    .HasName("PK__ESTUDIAN__C6A208F21C4EAFB6");

                entity.ToTable("ESTUDIANTE_GRUPO");

                entity.Property(e => e.Estudiante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.EstudianteGrupo)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__ESTUDIANT__Id_gr__74AE54BC");
            });

            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.ToTable("EVALUACION");

                entity.HasIndex(e => new { e.IdRubro, e.Nombre })
                    .HasName("EVALUACION_index_5")
                    .IsUnique();

                entity.Property(e => e.FechaEntrega)
                    .IsRequired()
                    .HasColumnName("Fecha_entrega")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.IdEspecificacion).HasColumnName("Id_especificacion");

                entity.Property(e => e.IdRubro).HasColumnName("Id_rubro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NotasPublicadas).HasColumnName("Notas_publicadas");

                entity.Property(e => e.PesoNota)
                    .HasColumnName("Peso_nota")
                    .HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.IdEspecificacionNavigation)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.IdEspecificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EVALUACIO__Id_es__7B5B524B");

                entity.HasOne(d => d.IdRubroNavigation)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.IdRubro)
                    .HasConstraintName("FK__EVALUACIO__Id_ru__778AC167");
            });

            modelBuilder.Entity<EvaluacionIntegrantes>(entity =>
            {
                entity.HasKey(e => new { e.Estudiante, e.IdEvaluacion })
                    .HasName("PK__EVALUACI__73BA28F6905D14CF");

                entity.ToTable("EVALUACION_INTEGRANTES");

                entity.Property(e => e.Estudiante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdEvaluacion).HasColumnName("Id_evaluacion");

                entity.HasOne(d => d.IdEvaluacionNavigation)
                    .WithMany(p => p.EvaluacionIntegrantes)
                    .HasForeignKey(d => d.IdEvaluacion)
                    .HasConstraintName("FK__EVALUACIO__Id_ev__787EE5A0");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.ToTable("GRUPO");

                entity.HasIndex(e => new { e.IdCurso, e.IdSemestre })
                    .HasName("GRUPO_index_2")
                    .IsUnique();

                entity.Property(e => e.IdCurso).HasColumnName("Id_curso");

                entity.Property(e => e.IdSemestre).HasColumnName("Id_semestre");

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => d.IdCurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GRUPO__Id_curso__71D1E811");

                entity.HasOne(d => d.IdSemestreNavigation)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => d.IdSemestre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GRUPO__Id_semest__72C60C4A");
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.ToTable("NOTICIA");

                entity.HasIndex(e => new { e.IdGrupo, e.Titulo, e.FechaPublicacion })
                    .HasName("NOTICIA_index_3")
                    .IsUnique();

                entity.Property(e => e.Autor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPublicacion)
                    .IsRequired()
                    .HasColumnName("Fecha_publicacion")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__NOTICIA__Id_grup__75A278F5");
            });

            modelBuilder.Entity<ProfesorGrupo>(entity =>
            {
                entity.HasKey(e => new { e.Profesor, e.IdGrupo })
                    .HasName("PK__PROFESOR__026E6B3C968B0D55");

                entity.ToTable("PROFESOR_GRUPO");

                entity.Property(e => e.Profesor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.ProfesorGrupo)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__PROFESOR___Id_gr__73BA3083");
            });

            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.ToTable("RUBRO");

                entity.HasIndex(e => new { e.IdGrupo, e.Nombre })
                    .HasName("RUBRO_index_4")
                    .IsUnique();

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Porcentaje).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Rubro)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__RUBRO__Id_grupo__76969D2E");
            });

            modelBuilder.Entity<Semestre>(entity =>
            {
                entity.ToTable("SEMESTRE");

                entity.HasIndex(e => new { e.Anio, e.Periodo })
                    .HasName("SEMESTRE_index_0")
                    .IsUnique();

                entity.Property(e => e.Periodo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
