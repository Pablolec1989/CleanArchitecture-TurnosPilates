using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TarifaService
{
    public class UpdateTarifaService<TDTO>
    {
        private readonly ICrudRepository<Tarifa> _tarifaRepositorio;
        private readonly IMapper<TDTO, Tarifa> _mapper;

        public UpdateTarifaService(ICrudRepository<Tarifa> tarifaRepositorio,
                                    IMapper<TDTO, Tarifa> mapper)
        {
            _tarifaRepositorio = tarifaRepositorio;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(int id, TDTO tarifaRequestDTO)
        {
            var tarifaAActualizar = _mapper.ToEntity(tarifaRequestDTO);
            tarifaAActualizar.Id = id;

            await _tarifaRepositorio.UpdateAsync(id, tarifaAActualizar);

        }
    }
}
