using ApplicationLayer;
using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using InterfaceAdapters_Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapter_Repository
{
    public class RepositoryInstructor : ICrudRepository<Instructor>
    {
        private readonly AppDbContext _dBcontext;

        public RepositoryInstructor(AppDbContext context)
        {
            _dBcontext = context;
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            var instructorModel = await _dBcontext.Instructores.FindAsync(id);

            if(instructorModel is null)
            {
                return null;
            }

            return new Instructor
            {
                Id = instructorModel.Id,
                Nombre = instructorModel.Nombre,
                Apellido = instructorModel.Apellido,
                NroTelefono = instructorModel.NroTelefono,
                PorcentajeDePago = instructorModel.PorcentajeDePago
            };
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _dBcontext.Instructores
                .Select(i => new Instructor
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Apellido = i.Apellido,
                    NroTelefono = i.NroTelefono,
                    PorcentajeDePago = i.PorcentajeDePago

                }).ToListAsync();
        }

        public async Task AddAsync(Instructor instructor)
        {
            var instructorModel = new InstructorModel
            {
                Id = instructor.Id,
                Nombre = instructor.Nombre,
                Apellido = instructor.Apellido,
                NroTelefono = instructor.NroTelefono,
                PorcentajeDePago = instructor.PorcentajeDePago
            };

            await _dBcontext.Instructores.AddAsync(instructorModel);
            await _dBcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Instructor instructor)
        {
            var instructorModel = await _dBcontext.Instructores.FindAsync(id);

            if(instructorModel != null)
            {
                instructorModel.Nombre = instructor.Nombre;
                instructorModel.Apellido = instructor.Apellido;
                instructorModel.NroTelefono = instructor.NroTelefono;
                instructorModel.PorcentajeDePago = instructor.PorcentajeDePago;
            };

            await _dBcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await _dBcontext.Instructores
                .Where(i => i.Id == id)
                .ExecuteDeleteAsync();

            await _dBcontext.SaveChangesAsync();

        }
    }
}
