using EnterpriseLayer_Entities;

namespace ApplicationLayer.AlumnoService
{
    public class GetAlumnoService<TEntity, TOutput>
    {
        private readonly ICrudRepository<Alumno> _alumnoRepository;

        public GetAlumnoService(ICrudRepository<Alumno> alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        public async Task<IEnumerable<Alumno>> ExecuteAsync()
        {
            return await _alumnoRepository.GetAllAsync();
        }

    }
}
