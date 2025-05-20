using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TurnoService
{
    public class DeleteTurnoService
    {
        private readonly ICrudRepository<Turno> _turnoRepository;

        public DeleteTurnoService(ICrudRepository<Turno> turnoRepository)
        {
            _turnoRepository = turnoRepository;
        }

        public async Task ExecuteAsync(int id)
        {
            var turnoExiste = await _turnoRepository.GetByIdAsync(id);

            if(turnoExiste is null)
            {
                throw new ValidationException("El turno no existe");
            }

            await _turnoRepository.DeleteAsync(id);

        }
    }
}
