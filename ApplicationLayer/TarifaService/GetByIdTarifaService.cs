using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TarifaService
{
    public class GetByIdTarifaService
    {
        private readonly ICrudRepository<Tarifa> _tarifaRepositorio;

        public GetByIdTarifaService(ICrudRepository<Tarifa> tarifaRepository)
        {
            _tarifaRepositorio = tarifaRepository;
        }

        public async Task<Tarifa> ExecuteAsync(int id)
        {
            var tarifa = await _tarifaRepositorio.GetByIdAsync(id);

            if (tarifa is null)
            {
                throw new ValidationException("No se encontró esa tarifa");
            }
            return tarifa;
        }
    }
}
