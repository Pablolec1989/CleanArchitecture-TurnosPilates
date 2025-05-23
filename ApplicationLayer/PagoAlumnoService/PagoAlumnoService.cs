using ApplicationLayer.AlumnoService;
using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.PagoAlumnoService
{
    public class PagoAlumnoService : IPagoAlumnoService<PagoAlumno>
    {
        private readonly ICrudRepository<Alumno> _alumnoRepository;
        private readonly ICrudRepository<Tarifa> _tarifaRepository;
        private readonly IPagoAlumnoRepository _pagoAlumnoRepository;

        public PagoAlumnoService(ICrudRepository<Alumno> alumnoRepository,
                                ICrudRepository<Tarifa> tarifaRepository,
                                IPagoAlumnoRepository pagoAlumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
            _tarifaRepository = tarifaRepository;
            _pagoAlumnoRepository = pagoAlumnoRepository;
        }

        public async Task<PagoAlumno> GenerarOActualizarPagoParaAlumno(int alumnoId)
        {
            // 1. Obtener el Alumno con sus inscripciones a turnos
            // Este método en IAlumnoRepository debe cargar la colección 'Turnos' de Alumno.
            var alumno = await _alumnoRepository.GetByIdAsync(alumnoId);

            if (alumno == null)
            {
                // Podrías lanzar una excepción personalizada si el alumno no existe
                throw new ArgumentException($"Alumno con ID {alumnoId} no encontrado.");
            }

            // 2. Calcular la cantidad de turnos en los que el alumno está inscrito
            // Asegúrate de que alumno.Turnos contenga los TurnosAlumnos correctamente.
            var cantidadTurnosInscrito = alumno.Turnos?.Count ?? 0;

            // 3. Obtener todas las tarifas disponibles
            var tarifas = await _tarifaRepository.GetAllAsync(); // Asumo que ITarifaRepository tiene un GetAllAsync()
            
            if (!tarifas.Any())
            {
                throw new InvalidOperationException("Aún no hay tarifas configuradas");
            }

            Tarifa tarifaAplicable;

            // 1. Intentar encontrar la tarifa exacta para 2 o 3 turnos
            if (cantidadTurnosInscrito > 1 && cantidadTurnosInscrito <= 3)
            {
                tarifaAplicable = tarifas.FirstOrDefault(t => t.Frecuencia_turno == cantidadTurnosInscrito);
            }
            else if (cantidadTurnosInscrito == 1 && cantidadTurnosInscrito >= 4);
            {
                // 2. Para 4 o más turnos, aplicar la tarifa unitaria (Frecuencia_turno = 1)
                tarifaAplicable = tarifas.FirstOrDefault(t => t.Frecuencia_turno == 1);
            }

            // Si después de toda la lógica, no se encontró la tarifa aplicable (ej. no existe una tarifa con Frecuencia_turno = 1)
            if (tarifaAplicable is null)
            {
                throw new InvalidOperationException("No se encontró la tarifa aplicable para la cantidad de turnos del alumno. Asegúrese de que existe una tarifa con Frecuencia_turno = 1.");
            }

            // 5. Intentar obtener el pago existente para este alumno
            // Debido a la relación 1:1, solo puede haber un PagoAlumno por AlumnoId.
            var pagoExistente = await _pagoAlumnoRepository.GetByAlumnoIdAsync(alumnoId);

            if (pagoExistente == null)
            {
                // Si no existe, crear un nuevo registro de pago
                var nuevoPago = new PagoAlumno
                {
                    AlumnoId = alumno.Id,
                    TarifaId = tarifaAplicable.Id,
                    Monto = tarifaAplicable.Precio,
                    Pagado = false, // Por defecto, el pago está pendiente
                };
                await _pagoAlumnoRepository.AddAsync(nuevoPago);
                return nuevoPago;
            }
            else
            {
                // Si ya existe, actualizar el registro de pago existente
                // Solo actualizamos el monto y la tarifa a la que se asocia
                pagoExistente.TarifaId = tarifaAplicable.Id;
                pagoExistente.Monto = tarifaAplicable.Precio;

                // Decisión de negocio: Si el monto cambia, ¿el pago se vuelve "no pagado"?
                // Si sí, descomenta la siguiente línea:
                // pagoExistente.Pagado = false;

                await _pagoAlumnoRepository.UpdateAsync(pagoExistente);
                return pagoExistente;
            }
        }
    }
}
