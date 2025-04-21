using EnterpriseLayer_Entities;

namespace ApplicationLayer.AlumnoService
{
    public class GetAlumnoService<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _alumnoRepository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetAlumnoService(IRepository<TEntity> alumnoRepository, IPresenter<TEntity, TOutput> presenter )
        {
            _alumnoRepository = alumnoRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var alumnos = await _alumnoRepository.GetAllAsync();
            return _presenter.Present(alumnos);
        }

    }
}
