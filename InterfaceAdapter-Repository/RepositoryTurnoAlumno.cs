using ApplicationLayer;
using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using InterfaceAdapters_Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InterfaceAdapter_Repository
{
    public class RepositoryTurnoAlumno : ITurnoAlumnoRepository
    {
        private readonly AppDbContext _dbContext;

        public RepositoryTurnoAlumno(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAlumnosToTurnoAsync(int turnoId, IEnumerable<int> alumnoIds)
        {
            var relaciones = alumnoIds.Select(alumnoId => new TurnoAlumnoModel
            {
                AlumnoId = alumnoId,
                TurnoId = turnoId

            }).ToList();

            await _dbContext.TurnosAlumnos.AddRangeAsync(relaciones);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAlumnosToTurnoAsync(int turnoId, IEnumerable<int> alumnoIds)
        {
            var relacionesActualesDelTurno = await _dbContext.TurnosAlumnos
                .Where(ta => ta.TurnoId == turnoId)
                .ToListAsync();

            // Obtener los IDs de los alumnos actualmente relacionados para la comparación
            var alumnosActualesIds = relacionesActualesDelTurno.Select(ta => ta.AlumnoId).ToList();

            // Identificar los IDs de los alumnos a eliminar (los que están en la lista actual pero no en la nueva)
            var alumnoIdsAEliminar = alumnosActualesIds.Except(alumnoIds).ToList();

            // Identificar los IDs de los alumnos a agregar (los que están en la nueva lista pero no en la actual)
            var alumnoIdsAAgregar = alumnoIds.Except(alumnosActualesIds).ToList();

            // Eliminar las relaciones existentes que ya no están en la nueva lista
            if (alumnoIdsAEliminar.Any())
            {
                // Filtra las entidades *ya rastreadas* por el DbContext
                var relacionesARemover = relacionesActualesDelTurno
                    .Where(r => alumnoIdsAEliminar.Contains(r.AlumnoId))
                    .ToList(); // Convertir a lista para evitar problemas de enumeración durante la eliminación

                _dbContext.TurnosAlumnos.RemoveRange(relacionesARemover);
            }

            // Agregar las nuevas relaciones
            if (alumnoIdsAAgregar.Any())
            {
                var relacionesAAdd = alumnoIdsAAgregar.Select(alumnoId => new TurnoAlumnoModel
                {
                    AlumnoId = alumnoId,
                    TurnoId = turnoId
                }).ToList();

                await _dbContext.TurnosAlumnos.AddRangeAsync(relacionesAAdd);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
