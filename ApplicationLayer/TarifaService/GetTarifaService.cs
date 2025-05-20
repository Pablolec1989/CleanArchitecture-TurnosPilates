using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TarifaService
{
    public class GetTarifaService
    {
        private readonly ICrudRepository<Tarifa> _tarifaRepositorio;

        public GetTarifaService(ICrudRepository<Tarifa> tarifaRepository)
        {
            _tarifaRepositorio = tarifaRepository;
        }

        public async Task<IEnumerable<Tarifa>> Get()
        {
            return await _tarifaRepositorio.GetAllAsync();
        }
    }
}
