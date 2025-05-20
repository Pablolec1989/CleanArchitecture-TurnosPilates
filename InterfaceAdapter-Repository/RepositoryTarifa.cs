using ApplicationLayer;
using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using InterfaceAdapters_Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapter_Repository
{
    public class RepositoryTarifa : ICrudRepository<Tarifa>
    {
        private readonly AppDbContext _dbContext;

        public RepositoryTarifa(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Tarifa tarifa)
        {
            var tarifaModel = new TarifaModel
            {
                Frecuencia_turno = tarifa.Frecuencia_turno,
                Precio = tarifa.Precio
            };

            await _dbContext.AddAsync(tarifaModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tarifaExiste = await _dbContext.Tarifas.Where(t => t.Id == id).ExecuteDeleteAsync();

            if (tarifaExiste == 0)
            {
                throw new Exception("No se pudo eliminar la tarifa");
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tarifa>> GetAllAsync()
        {
            var tarifas = await _dbContext.Tarifas.ToListAsync();

            return tarifas.Select(t => new Tarifa
            {
                Id = t.Id,
                Frecuencia_turno = t.Frecuencia_turno,
                Precio = t.Precio

            }).ToList() ?? new List<Tarifa>();
        }

        public async Task<Tarifa> GetByIdAsync(int id)
        {
            var tarifaModel = await _dbContext.Tarifas.FirstOrDefaultAsync(t => t.Id == id);

            if (tarifaModel is null)
            {
                return null;
            }

            return new Tarifa
            {
                Id = tarifaModel.Id,
                Frecuencia_turno = tarifaModel.Frecuencia_turno,
                Precio = tarifaModel.Precio,
            };
        }

        public async Task UpdateAsync(int id, Tarifa tarifa)
        {
            var tarifaModel = await _dbContext.Tarifas.FindAsync(id);

            if (tarifaModel != null)
            {
                tarifaModel.Frecuencia_turno = tarifa.Frecuencia_turno;
                tarifaModel.Precio = tarifa.Precio;

                await _dbContext.SaveChangesAsync();
            }


        }
    }
}
