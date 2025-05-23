using ApplicationLayer.Exception;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TurnoService
{
    public class UpdateTurnoService<TDTO>
    {
        private readonly ICrudRepository<Turno> _turnoRepository;
        private readonly IMapper<TDTO, Turno> _mapper;
        private readonly ITurnoAlumnoRepository _turnoAlumnoRepository;
        private readonly IPagoAlumnoService<PagoAlumno> _pagoAlumnoService;

        public UpdateTurnoService(ICrudRepository<Turno> turnoRepository, 
                                  IMapper<TDTO, Turno> mapper,
                                  ITurnoAlumnoRepository turnoAlumnoRepository,
                                  IPagoAlumnoService<PagoAlumno> pagoAlumnoService)
        {
            _turnoRepository = turnoRepository;
            _mapper = mapper;
            _turnoAlumnoRepository = turnoAlumnoRepository;
            _pagoAlumnoService = pagoAlumnoService;
        }

        public async Task ExecuteAsync(int id, TDTO turnoRequestDTO, List<int> alumnoIdsIniciales = null)
        {

            var turnoExistente = await _turnoRepository.GetByIdAsync(id);

            var turno = _mapper.ToEntity(turnoRequestDTO);
            turno.Id = id;

            await _turnoRepository.UpdateAsync(id, turno);

            //Actualizacion de tabla TurnosAlumnos
            if (alumnoIdsIniciales != null && alumnoIdsIniciales.Any())
            {
                await _turnoAlumnoRepository.UpdateAlumnosToTurnoAsync(turno.Id, alumnoIdsIniciales);
            }

            //Actualizar el pago del alumno
            if (turnoExistente != null && turnoExistente.InstructorId != turno.InstructorId)
            {
                // Si el instructor ha cambiado, actualiza el pago del alumno
                foreach (var alumno in turno.Alumnos)
                {
                    var pago = await _pagoAlumnoService.GenerarOActualizarPagoParaAlumno(alumno.AlumnoId);
                    // Aquí podrías hacer algo con el pago, como guardarlo o enviarlo a otro servicio
                }
            }

        }
    }
}
