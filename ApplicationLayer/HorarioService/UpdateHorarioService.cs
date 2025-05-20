using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.HorarioService
{
    public class UpdateHorarioService<TDTO>
    {
        private readonly ICrudRepository<Horario> _horarioRepository;
        private readonly IMapper<TDTO, Horario> _mapper;

        public UpdateHorarioService(ICrudRepository<Horario> horarioRepository, IMapper<TDTO, Horario> mapper)
        {
            _horarioRepository = horarioRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(int id, TDTO horarioRequestDTO)
        {
            var horarioExistente = await _horarioRepository.GetByIdAsync(id);

            if(horarioExistente is null)
            {
                throw new ValidationException($"No se encontró ningún horario con el ID: {id} para actualizar.");
            }

            var horarioActualizado = _mapper.ToEntity(horarioRequestDTO);
            horarioActualizado.Id = id;

            await _horarioRepository.UpdateAsync(id, horarioActualizado);
        }
    }
}
