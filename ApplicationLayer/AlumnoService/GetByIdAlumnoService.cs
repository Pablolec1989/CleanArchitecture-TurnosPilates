using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.AlumnoService
{
    public class GetByIdAlumnoService<Alumno, AlumnoEnTurnoViewModel>
    {
        private readonly IRepository<Alumno> _alumnoRepository;
        private readonly IPresenterById<Alumno, AlumnoEnTurnoViewModel> _presenter;

        public GetByIdAlumnoService(IRepository<Alumno> alumnoRepository, IPresenterById<Alumno, AlumnoEnTurnoViewModel> presenter)
        {
            _alumnoRepository = alumnoRepository;
            _presenter = presenter;
        }

        public async Task<AlumnoEnTurnoViewModel> ExecuteAsync(int id)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id);

            if (alumno is null)
            {
                throw new ValidationException("No se encontró el alumno");
            }

            return _presenter.Present(alumno);

        }
    }
}
