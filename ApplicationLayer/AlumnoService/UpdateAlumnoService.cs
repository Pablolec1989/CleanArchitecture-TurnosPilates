using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.AlumnoService
{
    public class UpdateAlumnoService<TDTO>
    {
        private readonly IRepository<Alumno> _alumnoRepository;
        private readonly IMapper<TDTO, Alumno> _mapper;

        public UpdateAlumnoService(IRepository<Alumno> alumnoRepository, 
                                    IMapper<TDTO, Alumno> mapper)
        {
            _alumnoRepository = alumnoRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(int id, TDTO alumnoRequestDTO)
        {
            var alumnoExistente = await _alumnoRepository.GetByIdAsync(id); //--busco el dato en el repo.

            if (alumnoExistente is null)
            {
                throw new ValidationException($"No se encontró ningún alumno con el ID: {id} para actualizar.");
            }

            var alumnoAActualizar = _mapper.ToEntity(alumnoRequestDTO);
            alumnoAActualizar.Id = id;

            await _alumnoRepository.UpdateAsync(id, alumnoAActualizar);
        }
    }
}
