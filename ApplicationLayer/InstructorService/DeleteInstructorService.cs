using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InstructorService
{
    public class DeleteInstructorService
    {
        private readonly ICrudRepository<Instructor> _instructorRepositorio;

        public DeleteInstructorService(ICrudRepository<Instructor> instructorRepositorio)
        {
            _instructorRepositorio = instructorRepositorio;
        }

        public async Task ExecuteAsync(int id)
        {
            var instructorExistente = await _instructorRepositorio.GetByIdAsync(id);

            if(instructorExistente is null)
            {
                throw new ValidationException("El instructor no existe");
            }

            await _instructorRepositorio.DeleteAsync(id);
        }
    }
}
