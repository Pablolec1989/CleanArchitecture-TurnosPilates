using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TurnoService
{
    public class AddTurnoService<TDTO>
    {
        private readonly IRepository<Turno> _turnoRepository;
        private readonly IMapper<TDTO, Turno> _mapper;

        public AddTurnoService(IRepository<Turno> turnoRepository, IMapper<TDTO, Turno> mapper)
        {
            _turnoRepository = turnoRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO turnoDTO)
        {
            var turno = _mapper.ToEntity(turnoDTO); //Convierto el DTO a TurnoModel para guardarlo en la BD
            await _turnoRepository.AddAsync(turno);
        }
    }
}
