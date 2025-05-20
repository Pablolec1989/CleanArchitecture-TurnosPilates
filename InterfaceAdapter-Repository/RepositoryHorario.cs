using ApplicationLayer;
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
    public class RepositoryHorario : ICrudRepository<Horario>
    {
        private readonly AppDbContext _dbContext;
        public RepositoryHorario(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Horario> GetByIdAsync(int id)
        {
            var horarioModel = await _dbContext.Horarios.FindAsync(id);

            if (horarioModel is null)
            {
                return null;
            }

            return new Horario()
            {
                Dia = horarioModel.Dia,
                Hora = horarioModel.Hora,
            };
        }
        public async Task<IEnumerable<Horario>> GetAllAsync()
        {
            return await _dbContext.Horarios
                .Select(h => new Horario
                {
                    Id = h.Id,
                    Dia = h.Dia,
                    Hora = h.Hora,
                })
                .ToListAsync();
        }
        public async Task AddAsync(Horario horario)
        {
            var horarioModel = new HorarioModel()
            {
                Dia = horario.Dia,
                Hora = horario.Hora,
            };

            await _dbContext.AddAsync(horarioModel);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, Horario horario)
        {
            var horarioModel = await _dbContext.Horarios.FindAsync(id);

            if(horarioModel != null)
            {
                horarioModel.Id = horario.Id;
                horarioModel.Dia = horario.Dia;
                horarioModel.Hora = horario.Hora;
            }

            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var horarioExiste = await _dbContext.Horarios
                .Where(h => h.Id == id).ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();
        }
    }
}
