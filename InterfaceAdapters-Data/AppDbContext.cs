using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using Microsoft.EntityFrameworkCore;

namespace InterfaceAdapters_Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<AlumnoModel> Alumnos { get; set; }
        public DbSet<InstructorModel> Instructores { get; set; }
        public DbSet<HorarioModel> Horarios { get; set; }
        public DbSet<TurnoModel> Turnos { get; set; }
        public DbSet<TurnoAlumnoModel> TurnosAlumnos { get; set; }
        public DbSet<TarifaModel> Tarifas { get; set; }
        public DbSet<PagoAlumnoModel> PagoAlumno { get; set; }

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
                    .HasMaxLength(10);

                entity.Property(h => h.Hora)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            // Configuración de TurnoModel
            modelBuilder.Entity<TurnoModel>(entity =>
            {
                entity.ToTable("Turno");

                entity.Property(t => t.Capacidad);

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

            // Configurar nombres de columnas FK explícitamente en Turno
            modelBuilder.Entity<TurnoModel>()
                .Property(t => t.InstructorId)
                .HasColumnName("InstructorId");

            modelBuilder.Entity<TurnoModel>()
                .Property(t => t.HorarioId)
                .HasColumnName("HorarioId");

            //Configuracion de TurnoAlumno
            modelBuilder.Entity<TurnoAlumnoModel>(entity =>
            {
                entity.ToTable("TurnoAlumno");
                entity.HasKey(ta => new { ta.AlumnoId, ta.TurnoId })
                .HasName("PK_TurnoAlumno"); // Clave compuesta

                // Relación con Alumno
                entity.HasOne(ta => ta.Alumno)
                      .WithMany(a => a.TurnoAlumno)
                      .HasForeignKey(ta => ta.AlumnoId)
                      .HasConstraintName("FK_TurnoAlumno_Alumno")
                      .OnDelete(DeleteBehavior.Cascade);

                // Relación con Turno
                entity.HasOne(ta => ta.Turno)
                      .WithMany(t => t.TurnoAlumno)
                      .HasForeignKey(ta => ta.TurnoId)
                      .HasConstraintName("FK_TurnoAlumno_Alumno")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuracion de Tarifa
            modelBuilder.Entity<TarifaModel>(entity =>
            {
                entity.ToTable("Tarifas");

                entity.HasKey(t => t.Id);
                entity.Property(t => t.Frecuencia_turno).IsRequired();
                entity.Property(t => t.Precio)
                .HasColumnType("decimal(18,2)").IsRequired();
            });

            // Configuracion de PagoAlumno
            modelBuilder.Entity<PagoAlumnoModel>(entity =>
            {
                entity.ToTable("PagoAlumno");
                entity.HasKey(pa => pa.AlumnoId);

                entity.Property(pa => pa.Monto)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                // Configuración de la relación 1:1 con Alumno
                entity.HasOne(pa => pa.Alumno)          
                      .WithOne()                       
                      .HasForeignKey<PagoAlumnoModel>(pa => pa.AlumnoId) // AlumnoId en PagoAlumno es la FK y PK
                      .OnDelete(DeleteBehavior.Cascade);

                // Configuración de la relación 1:M con Tarifa (un PagoAlumno se basa en UNA Tarifa)
                entity.HasOne(pa => pa.Tarifa)
                      .WithMany()
                      .HasForeignKey(pa => pa.TarifaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
