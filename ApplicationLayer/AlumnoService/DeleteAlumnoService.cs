using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.AlumnoService
{
    public class DeleteAlumnoService
    {
        private readonly ICrudRepository<Alumno> _alumnoRepositorio;

        public DeleteAlumnoService(ICrudRepository<Alumno> alumnoRepositorio)
        {
            _alumnoRepositorio = alumnoRepositorio;
        }

        public async Task ExecuteAsync(int id)
        {
            var alumnoExistente = await _alumnoRepositorio.GetByIdAsync(id);

            if(alumnoExistente is null)
            {
                throw new ValidationException("El alumno no existe");
            }

            await _alumnoRepositorio.DeleteAsync(id);
        }
    }
}
