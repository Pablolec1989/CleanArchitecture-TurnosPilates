using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TurnoService
{
    public class GetTurnoService<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _turnoRepository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetTurnoService(IRepository<TEntity> turnoRepository, IPresenter<TEntity, TOutput> presenter)
        {
            _turnoRepository = turnoRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var turnos = await _turnoRepository.GetAllAsync();
            return _presenter.Present(turnos);
        }
    }
}
