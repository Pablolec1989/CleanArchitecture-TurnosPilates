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
                Observaciones = a.Observaciones,
                NroTelefono = a.NroTelefono,
            });
        }
    }
}
