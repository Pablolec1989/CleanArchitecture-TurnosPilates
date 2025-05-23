using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TurnoService
{
    public class DeleteTurnoService
    {
        private readonly ICrudRepository<Turno> _turnoRepository;
        private readonly ITurnoAlumnoRepository _turnoAlumnoRepository;
        private readonly IPagoAlumnoService<PagoAlumno> _pagoAlumnoService;

        public DeleteTurnoService(ICrudRepository<Turno> turnoRepository, 
                                    ITurnoAlumnoRepository turnoAlumnoRepository,
                                    IPagoAlumnoService<PagoAlumno> pagoAlumnoService)
        {
            _turnoRepository = turnoRepository;
            _turnoAlumnoRepository = turnoAlumnoRepository;
            _pagoAlumnoService = pagoAlumnoService;
        }

        public async Task ExecuteAsync(int id, List<int> alumnoIdsIniciales = null)
        {
            var turnoExiste = await _turnoRepository.GetByIdAsync(id);

            if (turnoExiste is null)
            {
                throw new ValidationException("El turno no existe");
            }

            await _turnoRepository.DeleteAsync(id);

            //Eliminar los registros de la tabla TurnoAlumno
            if (alumnoIdsIniciales != null && alumnoIdsIniciales.Count > 0)
            {
                // Eliminar los alumnos del turno
                await _turnoAlumnoRepository.DeleteAlumnoEnTurnoAsync(id, alumnoIdsIniciales);
            }

            //Actualizar el pago para cada alumno
            if (alumnoIdsIniciales != null && alumnoIdsIniciales.Any())
            {
                foreach (var alumnoId in alumnoIdsIniciales)
                {
                    await _pagoAlumnoService.GenerarOActualizarPagoParaAlumno(alumnoId);
                }
            }

        }
    }
}
