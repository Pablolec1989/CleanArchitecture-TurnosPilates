using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InstructorService
{
    public class UpdateInstructorService<TDTO>
    {
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IMapper<TDTO, Instructor> _mapper;

        public UpdateInstructorService(IRepository<Instructor> instructorRepository, 
                                        IMapper<TDTO, Instructor> mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(int id, TDTO instructorRequestDTO)
        {
            var instructorExiste = await _instructorRepository.GetByIdAsync(id);

            if(instructorExiste is null)
            {
                throw new ValidationException($"No se encontró ningún instructor con el ID: {id} para actualizar.");
            }

            var instructorAActualizar = _mapper.ToEntity(instructorRequestDTO);
            instructorAActualizar.Id = id;

            await _instructorRepository.UpdateAsync(id, instructorAActualizar);
        }
    }
}
