using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.HorarioService
{
    public class AddHorarioService<TDTO>
    {
        private readonly ICrudRepository<Horario> _horarioRepository;
        private readonly IMapper<TDTO, Horario> _mapper;

        public AddHorarioService(ICrudRepository<Horario> horarioRepository, IMapper<TDTO, Horario> mapper)
        {
            _horarioRepository = horarioRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO horarioDTO)
        {
            var horario = _mapper.ToEntity(horarioDTO);
            await _horarioRepository.AddAsync(horario);
        }
    }
}
