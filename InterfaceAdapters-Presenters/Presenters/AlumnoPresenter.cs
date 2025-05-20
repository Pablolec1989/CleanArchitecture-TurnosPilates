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
    public class AlumnoPresenter : IPresenter<Alumno, AlumnoViewModel>
    {
        public IEnumerable<AlumnoViewModel> Present(IEnumerable<Alumno> alumnos)
        {
            return alumnos.Select(a => new AlumnoViewModel
            {
                Id = a.Id,
                NombreCompleto = $"{a.Nombre} {a.Apellido}",
                Turnos = a.Turnos?.Select(ta => new TurnosAlumnos
                {
                    Turno = ta.Turno != null ? new Turno
                    {
                        Horario = new Horario
                        {
                            Dia = ta.Turno.Horario.Dia,
                            Hora = ta.Turno.Horario.Hora,
                        }
                    }  : null
                }).ToList() ?? new List<TurnosAlumnos>(),
            });
        }
    }
}
