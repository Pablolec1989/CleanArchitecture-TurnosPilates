using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.AlumnoService
{
    public class GetByIdAlumnoService
    {
        private readonly IRepository<Alumno> _alumnoRepository;

        public GetByIdAlumnoService(IRepository<Alumno> alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        public async Task<Alumno> ExecuteAsync(int id)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id);

            if(alumno is null)
            {
                throw new ValidationException($"No se encontró el alumno con el ID: {id}.");
            }

            return alumno;
        }
    }
}
