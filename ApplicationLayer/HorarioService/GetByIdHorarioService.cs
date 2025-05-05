using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.HorarioService
{
    public class GetByIdHorarioService
    {
        private readonly IRepository<Horario> _horarioRepository;

        public GetByIdHorarioService(IRepository<Horario> horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public async Task<Horario> ExecuteAsync(int id)
        {
            var horario = await _horarioRepository.GetByIdAsync(id);

            if(horario is null)
            {
                throw new ValidationException("No se encontró el horario");
            }

            return horario;
        }
    }
}
