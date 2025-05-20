using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TarifaService
{
    public class AddTarifaService<TDTO>
    {
        private readonly ICrudRepository<Tarifa> _tarifaRepositorio;
        private readonly IMapper<TDTO, Tarifa> _mapper;

        public AddTarifaService(ICrudRepository<Tarifa> tarifaRepository, IMapper<TDTO, Tarifa> mapper)
        {
            _tarifaRepositorio = tarifaRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO tarifaDTO)
        {
            var tarifa = _mapper.ToEntity(tarifaDTO);
            await _tarifaRepositorio.AddAsync(tarifa);
        }
    }
}
