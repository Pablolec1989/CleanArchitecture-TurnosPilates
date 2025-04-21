using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.HorarioService
{
    public class DeleteHorarioService
    {
        private readonly IRepository<Horario> _horarioRepository;

        public DeleteHorarioService(IRepository<Horario> horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public async Task ExecuteAsync(int id)
        {
            var horarioExiste = await _horarioRepository.GetByIdAsync(id);

            if(horarioExiste is null)
            {
                throw new ValidationException("El horario no existe");
            }
            await _horarioRepository.DeleteAsync(id);

        }
    }
}
