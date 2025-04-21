using ApplicationLayer;
using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using InterfaceAdapters_Data;
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
            var alumnoModel = await _dbContext.Alumnos.FindAsync(id);

            if(alumnoModel is null)
            {
                return null;
            }

            return new Alumno()
            {
                Id = alumnoModel.Id,
                Nombre = alumnoModel.Nombre,
                Apellido = alumnoModel.Apellido,
                Observaciones = alumnoModel.Observaciones,
                NroTelefono = alumnoModel.NroTelefono
            };
        }

        public async Task<IEnumerable<Alumno>> GetAllAsync()
        {
            return await _dbContext.Alumnos.
                Select(a => new Alumno()
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Apellido = a.Apellido,
                    Observaciones = a.Observaciones,
                    NroTelefono = a.NroTelefono,
                })
                .ToListAsync();
        }

        public async Task AddAsync(Alumno alumno)
        {
            var alumnoModel = new AlumnoModel() //---Se transforma la entidad a model.
            {
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                NroTelefono = alumno.NroTelefono
            };

            await _dbContext.Alumnos.AddAsync(alumnoModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Alumno alumno)
        {
            var alumnoModel = await _dbContext.Alumnos.FindAsync(id); //--busco el dato y piso sus prop.

            if(alumnoModel != null)
            {
                alumnoModel.Nombre = alumno.Nombre;
                alumnoModel.Apellido = alumno.Apellido;
                alumnoModel.Observaciones = alumno.Observaciones;
                alumnoModel.NroTelefono = alumno.NroTelefono;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var alumnoExiste = await _dbContext.Alumnos
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync(); 
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
