using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TarifaService
{
    public class DeleteTarifaService
    {
        private readonly ICrudRepository<Tarifa> _tarifaRepositorio;

        public DeleteTarifaService(ICrudRepository<Tarifa> tarifaRepository)
        {
            _tarifaRepositorio = tarifaRepository;
        }

        public async Task ExecuteAsync(int id)
        {
            var tarifa = await _tarifaRepositorio.GetByIdAsync(id);

            if(tarifa is null)
            {
                throw new ValidationException("La tarifa no existe");
            }

            await _tarifaRepositorio.DeleteAsync(id);
        }
    }
}
