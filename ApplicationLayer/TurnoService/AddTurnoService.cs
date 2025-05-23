using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.TurnoService
{
    public class AddTurnoService<TDTO>
    {
        private readonly ICrudRepository<Turno> _turnoRepository;
        private readonly IMapper<TDTO, Turno> _mapper;
        private readonly ITurnoAlumnoRepository _repositoryTurnoAlumno;
        private readonly IPagoAlumnoService<PagoAlumno> _pagoAlumnoService;

        public AddTurnoService(ICrudRepository<Turno> turnoRepository, 
                                IMapper<TDTO, Turno> mapper, 
                                ITurnoAlumnoRepository repositoryTurnoAlumno,
                                IPagoAlumnoService<PagoAlumno> pagoAlumnoService)
        {
            _turnoRepository = turnoRepository;
            _mapper = mapper;
            _repositoryTurnoAlumno = repositoryTurnoAlumno;
            _pagoAlumnoService = pagoAlumnoService;
        }

        
        public async Task ExecuteAsync(TDTO turnoDTO, List<int> alumnoIdsIniciales = null)
        {
            var turno = _mapper.ToEntity(turnoDTO);
            await _turnoRepository.AddAsync(turno);

            //Actualizo la tabla TurnosAlumnos
            if (alumnoIdsIniciales != null && alumnoIdsIniciales.Any())
            {
                await _repositoryTurnoAlumno.AddAlumnosToTurnoAsync(turno.Id, alumnoIdsIniciales);
            }

            //Generar o actualizar el pago para cada alumno
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
