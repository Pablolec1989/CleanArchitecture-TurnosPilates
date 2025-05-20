using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TurnoService
{
    public class GetByIdTurnoService
    {
        private readonly ICrudRepository<Turno> _turnoRepository;

        public GetByIdTurnoService(ICrudRepository<Turno> turnoRepository)
        {
            _turnoRepository = turnoRepository;
        }

        public async Task<Turno> ExecuteAsync(int id)
        {
            var turno = await _turnoRepository.GetByIdAsync(id);

            if (turno is null)
            {
                throw new ValidationException("No se encontró el turno");
            }
            return turno;
        }
    }
}
