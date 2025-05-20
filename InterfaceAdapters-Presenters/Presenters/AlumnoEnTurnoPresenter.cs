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
    public class AlumnoEnTurnoPresenter : IPresenterById<Alumno, AlumnoEnTurnoViewModel>
    {
        public AlumnoEnTurnoViewModel Present(Alumno entity)
        {
            return new AlumnoEnTurnoViewModel
            {
                Id = entity.Id,
                NombreCompleto = $"{entity.Nombre} {entity.Apellido}",
                HorariosTurnos = entity.Turnos?
                .Where(ta => ta.Turno?.Horario != null)
                .Select(ta => $"{ta.Turno.Horario.Dia} {ta.Turno.Horario.Hora}")
                .ToList() ?? new List<string>()

            };
        }
    }
}
