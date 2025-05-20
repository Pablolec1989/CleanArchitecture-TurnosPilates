using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InstructorService
{
    public class GetByIdInstructorService
    {
        private readonly ApplicationLayer.ICrudRepository<Instructor> _instructorRepository;

        public GetByIdInstructorService(ICrudRepository<Instructor> instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<Instructor> ExecuteAsync(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);

            if(instructor is null)
            {
                throw new ValidationException($"No se encontró el instructor con el ID: {id}.");
            }

            return instructor;
        }
    }
}
