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
        public virtual DbSet<CarpetaRaiz> CarpetaRaiz { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<CursoSemestre> CursoSemestre { get; set; }
        public virtual DbSet<Entregable> Entregable { get; set; }
        public virtual DbSet<EntregableArchivo> EntregableArchivo { get; set; }
        public virtual DbSet<EntregableArchivoDetalle> EntregableArchivoDetalle { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<EvaluacionEspecificacion> EvaluacionEspecificacion { get; set; }
        public virtual DbSet<EvaluacionIntegrantes> EvaluacionIntegrantes { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<GrupoEstudiante> GrupoEstudiante { get; set; }
        public virtual DbSet<GrupoProfesor> GrupoProfesor { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<Periodo> Periodo { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<Semestre> Semestre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:xtecdigitalcr.database.windows.net,1433;Initial Catalog=xtecdigital;Persist Security Info=False;User ID=xtec_admin;Password=Tjg*%Ui9BM5K;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.ToTable("ARCHIVO");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("Fecha_creacion")
                    .HasColumnType("date");

                entity.Property(e => e.IdCarpeta).HasColumnName("Id_carpeta");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tamanio).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.IdCarpetaNavigation)
                    .WithMany(p => p.Archivo)
                    .HasForeignKey(d => d.IdCarpeta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ARCHIVO__Id_carp__0F624AF8");
            });

            modelBuilder.Entity<Carpeta>(entity =>
            {
                entity.ToTable("CARPETA");

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ruta)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoloLectura).HasColumnName("Solo_lectura");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Carpeta)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARPETA__Id_grup__0D7A0286");
            });

            modelBuilder.Entity<CarpetaRaiz>(entity =>
            {
                entity.ToTable("CARPETA_RAIZ");

                entity.Property(e => e.IdCarpeta).HasColumnName("Id_carpeta");

                entity.HasOne(d => d.IdCarpetaNavigation)
                    .WithMany(p => p.CarpetaRaiz)
                    .HasForeignKey(d => d.IdCarpeta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARPETA_R__Id_ca__0E6E26BF");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__CURSO__06370DAD9E8CE09A");

                entity.ToTable("CURSO");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Carrera)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CursoSemestre>(entity =>
            {
                entity.ToTable("CURSO_SEMESTRE");

                entity.Property(e => e.IdCurso)
                    .IsRequired()
                    .HasColumnName("Id_curso")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdSemestre).HasColumnName("Id_semestre");

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.CursoSemestre)
                    .HasForeignKey(d => d.IdCurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CURSO_SEM__Id_cu__7F2BE32F");

                entity.HasOne(d => d.IdSemestreNavigation)
                    .WithMany(p => p.CursoSemestre)
                    .HasForeignKey(d => d.IdSemestre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CURSO_SEM__Id_se__00200768");
            });

            modelBuilder.Entity<Entregable>(entity =>
            {
                entity.ToTable("ENTREGABLE");

                entity.Property(e => e.Estudiante)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdEvaluacion).HasColumnName("Id_evaluacion");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEvaluacionNavigation)
                    .WithMany(p => p.Entregable)
                    .HasForeignKey(d => d.IdEvaluacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENTREGABL__Id_ev__10566F31");
            });

            modelBuilder.Entity<EntregableArchivo>(entity =>
            {
                entity.ToTable("ENTREGABLE_ARCHIVO");

                entity.Property(e => e.IdArchivo).HasColumnName("Id_archivo");

                entity.Property(e => e.IdEntregable).HasColumnName("Id_entregable");

                entity.HasOne(d => d.IdArchivoNavigation)
                    .WithMany(p => p.EntregableArchivo)
                    .HasForeignKey(d => d.IdArchivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENTREGABL__Id_ar__0A9D95DB");

                entity.HasOne(d => d.IdEntregableNavigation)
                    .WithMany(p => p.EntregableArchivo)
                    .HasForeignKey(d => d.IdEntregable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENTREGABL__Id_en__09A971A2");
            });

            modelBuilder.Entity<EntregableArchivoDetalle>(entity =>
            {
                entity.ToTable("ENTREGABLE_ARCHIVO_DETALLE");

                entity.Property(e => e.IdArchivoDetalle).HasColumnName("Id_archivo_detalle");

                entity.Property(e => e.IdEntregable).HasColumnName("Id_entregable");

                entity.HasOne(d => d.IdArchivoDetalleNavigation)
                    .WithMany(p => p.EntregableArchivoDetalle)
                    .HasForeignKey(d => d.IdArchivoDetalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENTREGABL__Id_ar__0C85DE4D");

                entity.HasOne(d => d.IdEntregableNavigation)
                    .WithMany(p => p.EntregableArchivoDetalle)
                    .HasForeignKey(d => d.IdEntregable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENTREGABL__Id_en__0B91BA14");
            });

            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.ToTable("EVALUACION");

                entity.Property(e => e.EsIndividual).HasColumnName("Es_individual");

                entity.Property(e => e.FechaEntrega)
                    .HasColumnName("Fecha_entrega")
                    .HasColumnType("date");

                entity.Property(e => e.HoraEntrega).HasColumnName("Hora_entrega");

                entity.Property(e => e.IdRubro).HasColumnName("Id_rubro");

                entity.Property(e => e.NotasPublicadas).HasColumnName("Notas_publicadas");

                entity.HasOne(d => d.IdRubroNavigation)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.IdRubro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EVALUACIO__Id_ru__05D8E0BE");
            });

            modelBuilder.Entity<EvaluacionEspecificacion>(entity =>
            {
                entity.ToTable("EVALUACION_ESPECIFICACION");

                entity.Property(e => e.IdEspecificacion).HasColumnName("Id_especificacion");

                entity.Property(e => e.IdEvaluacion).HasColumnName("Id_evaluacion");

                entity.HasOne(d => d.IdEspecificacionNavigation)
                    .WithMany(p => p.EvaluacionEspecificacion)
                    .HasForeignKey(d => d.IdEspecificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EVALUACIO__Id_es__08B54D69");

                entity.HasOne(d => d.IdEvaluacionNavigation)
                    .WithMany(p => p.EvaluacionEspecificacion)
                    .HasForeignKey(d => d.IdEvaluacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EVALUACIO__Id_ev__07C12930");
            });

            modelBuilder.Entity<EvaluacionIntegrantes>(entity =>
            {
                entity.HasKey(e => new { e.IdEvaluacion, e.Estudiante })
                    .HasName("PK__EVALUACI__46E284405D190C7D");

                entity.ToTable("EVALUACION_INTEGRANTES");

                entity.Property(e => e.IdEvaluacion).HasColumnName("Id_evaluacion");

                entity.Property(e => e.Estudiante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEvaluacionNavigation)
                    .WithMany(p => p.EvaluacionIntegrantes)
                    .HasForeignKey(d => d.IdEvaluacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EVALUACIO__Id_ev__06CD04F7");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.ToTable("GRUPO");

                entity.Property(e => e.IdCurso)
                    .IsRequired()
                    .HasColumnName("Id_curso")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => d.IdCurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GRUPO__Id_curso__02084FDA");
            });

            modelBuilder.Entity<GrupoEstudiante>(entity =>
            {
                entity.HasKey(e => new { e.NumeroGrupo, e.Estudiante })
                    .HasName("PK__GRUPO_ES__9E87469A3C277F4C");

                entity.ToTable("GRUPO_ESTUDIANTE");

                entity.Property(e => e.NumeroGrupo).HasColumnName("Numero_Grupo");

                entity.Property(e => e.Estudiante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.NumeroGrupoNavigation)
                    .WithMany(p => p.GrupoEstudiante)
                    .HasForeignKey(d => d.NumeroGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GRUPO_EST__Numer__03F0984C");
            });

            modelBuilder.Entity<GrupoProfesor>(entity =>
            {
                entity.HasKey(e => new { e.NumeroGrupo, e.Profesor })
                    .HasName("PK__GRUPO_PR__72CB80A6841FE148");

                entity.ToTable("GRUPO_PROFESOR");

                entity.Property(e => e.NumeroGrupo).HasColumnName("Numero_Grupo");

                entity.Property(e => e.Profesor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.NumeroGrupoNavigation)
                    .WithMany(p => p.GrupoProfesor)
                    .HasForeignKey(d => d.NumeroGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GRUPO_PRO__Numer__02FC7413");
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.ToTable("NOTICIA");

                entity.Property(e => e.Autor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPublicacion)
                    .HasColumnName("Fecha_publicacion")
                    .HasColumnType("date");

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NOTICIA__Id_grup__01142BA1");
            });

            modelBuilder.Entity<Periodo>(entity =>
            {
                entity.ToTable("PERIODO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.ToTable("RUBRO");

                entity.HasIndex(e => new { e.Nombre, e.IdGrupo })
                    .HasName("UQ__RUBRO__EDCB6D05B28DD2E6")
                    .IsUnique();

                entity.Property(e => e.IdGrupo).HasColumnName("Id_grupo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Porcentaje).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Rubro)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RUBRO__Id_grupo__04E4BC85");
            });

            modelBuilder.Entity<Semestre>(entity =>
            {
                entity.ToTable("SEMESTRE");

                entity.Property(e => e.IdPeriodo).HasColumnName("Id_periodo");

                entity.HasOne(d => d.IdPeriodoNavigation)
                    .WithMany(p => p.Semestre)
                    .HasForeignKey(d => d.IdPeriodo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SEMESTRE__Id_per__7E37BEF6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
