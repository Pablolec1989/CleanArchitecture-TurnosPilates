using ApplicationLayer;
using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using InterfaceAdapters_Data;
using InterfaceAdapters_Presenters.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace InterfaceAdapter_Repository
{
    public class RepositoryAlumno : IRepository<Alumno>
    {
        private readonly AppDbContext _dbContext;

        public RepositoryAlumno(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Alumno> GetByIdAsync(int id)
        {
            var alumnoModel = await _dbContext.Alumnos
                .Include(a => a.Turnos)
                    .ThenInclude(ta => ta.Turno)
                    .ThenInclude(t => t.Horario)
                .FirstOrDefaultAsync(a => a.Id == id);

            if(alumnoModel is null)
            {
                return null;
            }

            return new Alumno
            {
                Id = alumnoModel.Id,
                Nombre = alumnoModel.Nombre,
                Apellido = alumnoModel.Apellido,
                Observaciones = alumnoModel.Observaciones,
                NroTelefono = alumnoModel.NroTelefono,
                Turnos = alumnoModel.Turnos?.Select(ta => new TurnosAlumnos
                {
                    Turno = ta.Turno != null ? new Turno
                    {
                        Horario = new Horario
                        {
                            Dia = ta.Turno.Horario.Dia,
                            Hora = ta.Turno.Horario.Hora,
                        }
                    } : null

                }).ToList() ?? new List<TurnosAlumnos>(),

            };
        }

        public async Task<IEnumerable<Alumno>> GetAllAsync()
        {
            return await _dbContext.Alumnos
                .Include(a => a.Turnos)
                .Select(a => new Alumno()
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Apellido = a.Apellido,
                    Observaciones = a.Observaciones,
                    NroTelefono = a.NroTelefono,
                    Turnos = a.Turnos.Select(ta => new TurnosAlumnos
                    {
                        TurnoId = ta.TurnoId,
                        AlumnoId = ta.AlumnoId,

                    }).ToList() ?? new List<TurnosAlumnos>(),

                }).ToListAsync();
        }

        public async Task AddAsync(Alumno alumno)
        {
            var alumnoModel = new AlumnoModel
            {
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                Observaciones = alumno.Apellido,
                NroTelefono = alumno.NroTelefono,
            };

            await _dbContext.AddAsync(alumnoModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Alumno alumno)
        {
            var alumnoModel = await _dbContext.Alumnos.FindAsync(id);

            if(alumnoModel != null)
            {
                alumnoModel.Nombre = alumno.Nombre;
                alumnoModel.Apellido = alumno.Apellido;
                alumnoModel.Observaciones = alumno.Observaciones;
                alumnoModel.NroTelefono = alumno.NroTelefono;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var alumnoExiste = await _dbContext.Alumnos.Where(a => a.Id == id).ExecuteDeleteAsync(); 
            await _dbContext.SaveChangesAsync();
        }
    }
}
