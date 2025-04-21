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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlumnoModel>().ToTable("Alumno");
            modelBuilder.Entity<InstructorModel>().ToTable("Instructor");
            modelBuilder.Entity<HorarioModel>().ToTable("Horario");
        }
    }
}
