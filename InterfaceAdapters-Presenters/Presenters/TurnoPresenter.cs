using ApplicationLayer;
using EnterpriseLayer_Entities;
using InterfaceAdapters_Presenters.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters_Presenters.Presenters
{
    public class TurnoPresenter : IPresenter<Turno, TurnoViewModel>
    {
        public IEnumerable<TurnoViewModel> Present(IEnumerable<Turno> turnos)
        {
            return turnos.Select(t => new TurnoViewModel
            {
                Id = t.Id,
                Horario = t.Horario is null ? null : new HorarioViewModel
                {
                    Horario = $"{t.Horario.Dia} {t.Horario.Hora}"
                },
                Instructor = t.Instructor is null ? null : new InstructorViewModel
                {
                    Id = t.Instructor.Id,
                    NombreCompleto = $"{t.Instructor.Nombre} {t.Instructor.Apellido}",
                },
                Alumnos = t.Alumnos?.Select(ta => new AlumnoViewModel
                {
                    Id = ta.AlumnoId,
                    NombreCompleto = $"{ta.Alumno?.Nombre} {ta.Alumno?.Apellido}",

                }).ToList() ?? new List<AlumnoViewModel>(),

                Capacidad = t.Capacidad,
                Disponibilidad = t.Capacidad - (t.Alumnos?.Count()) ?? 0,
            });
        }
    }
}
