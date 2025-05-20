using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InstructorService
{
    public class AddInstructorService<TDTO>
    {
        private readonly ICrudRepository<Instructor> _instructorRepository;
        private readonly IMapper<TDTO, Instructor> _mapper;

        public AddInstructorService(ICrudRepository<Instructor> instructorRepository, 
                                    IMapper<TDTO, Instructor> mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO instructorDTO)
        {
            var instructor = _mapper.ToEntity(instructorDTO);

            //...validaciones?

            await _instructorRepository.AddAsync(instructor);
        }


    }
}
