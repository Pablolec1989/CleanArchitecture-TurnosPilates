using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using Microsoft.EntityFrameworkCore;

namespace InterfaceAdapters_Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<AlumnoModel> Alumnos { get; set; }
        public DbSet<InstructorModel> Instructores { get; set; }
        public DbSet<HorarioModel> Horarios { get; set; }
        public DbSet<TurnoModel> Turnos { get; set; }
        public DbSet<TurnoAlumnoModel> TurnosAlumnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de AlumnoModel
            modelBuilder.Entity<AlumnoModel>(entity =>
            {
                entity.ToTable("Alumno");

                entity.Property(a => a.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.NroTelefono)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            // Configuración de InstructorModel
            modelBuilder.Entity<InstructorModel>(entity =>
            {
                entity.ToTable("Instructor");

                entity.Property(i => i.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(i => i.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(i => i.NroTelefono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(i => i.PorcentajeDePago)
                    .IsRequired();
            });

            // Configuración de HorarioModel
            modelBuilder.Entity<HorarioModel>(entity =>
            {
                entity.ToTable("Horario");

                entity.Property(h => h.Dia)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(h => h.Hora)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            // Configuración de TurnoModel
            modelBuilder.Entity<TurnoModel>(entity =>
            {
                entity.ToTable("Turno");

                entity.Property(t => t.Capacidad)
                    .IsRequired();

                // Relación con Instructor
                entity.HasOne(t => t.Instructor)
                    .WithMany(i => i.Turnos)
                    .HasForeignKey(t => t.InstructorId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Relación con Horario
                entity.HasOne(t => t.Horario)
                    .WithMany(h => h.Turnos)
                    .HasForeignKey(t => t.HorarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de TurnoAlumnoModel
            modelBuilder.Entity<TurnoAlumnoModel>(entity =>
            {
                entity.ToTable("TurnoAlumno");

                // Clave primaria compuesta
                entity.HasKey(ta => new { ta.AlumnoId, ta.TurnoId });

                // Relación con Alumno
                entity.HasOne(ta => ta.Alumno)
                    .WithMany(a => a.Turnos)
                    .HasForeignKey(ta => ta.AlumnoId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación con Turno
                entity.HasOne(ta => ta.Turno)
                    .WithMany(t => t.Alumnos)
                    .HasForeignKey(ta => ta.TurnoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Opcional: Configurar nombres de columnas FK explícitamente
            modelBuilder.Entity<TurnoModel>()
                .Property(t => t.InstructorId)
                .HasColumnName("InstructorId");

            modelBuilder.Entity<TurnoModel>()
                .Property(t => t.HorarioId)
                .HasColumnName("HorarioId");
        }
    }
}
