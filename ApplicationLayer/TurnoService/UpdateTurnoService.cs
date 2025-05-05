using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TurnoService
{
    public class UpdateTurnoService<TDTO>
    {
        private readonly IRepository<Turno> _turnoRepository;
        private readonly IMapper<TDTO, Turno> _mapper;

        public UpdateTurnoService(IRepository<Turno> turnoRepository, IMapper<TDTO, Turno> mapper)
        {
            _turnoRepository = turnoRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(int id, TDTO turnoRequestDTO)
        {
            var turnoExistente = await _turnoRepository.GetByIdAsync(id);

            if (turnoExistente is null)
            {
                throw new ValidationException("No se encontró ningún alumno");
            }

            var turnoAActualizar = _mapper.ToEntity(turnoRequestDTO);
            turnoAActualizar.Id = id;

            await _turnoRepository.UpdateAsync(id, turnoAActualizar);
        }
    }
}
