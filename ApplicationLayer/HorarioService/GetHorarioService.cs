using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.HorarioService
{
    public class GetHorarioService<TEntity, TOutput>
    {
        private readonly ICrudRepository<TEntity> _horarioRepository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetHorarioService(ICrudRepository<TEntity> horarioRepository, IPresenter<TEntity, TOutput> presenter)
        {
            _horarioRepository = horarioRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var horarios = await _horarioRepository.GetAllAsync();
            return _presenter.Present(horarios);
        }
    }
}
