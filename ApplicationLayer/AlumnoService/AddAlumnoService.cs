using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.AlumnoService
{
    public class AddAlumnoService<TDTO>
    {
        private readonly ICrudRepository<Alumno> _alumnoRepository;
        private readonly IMapper<TDTO, Alumno> _mapper;

        public AddAlumnoService(ICrudRepository<Alumno> alumnoRepository, IMapper<TDTO, Alumno> mapper)
        {
            _alumnoRepository = alumnoRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO alumnoDTO)
        {
            var alumno = _mapper.ToEntity(alumnoDTO);
            await _alumnoRepository.AddAsync(alumno);
        }
    }
}
