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
    public class InstructorPresenter : IPresenter<Instructor, InstructorViewModel>
    {
        public IEnumerable<InstructorViewModel> Present(IEnumerable<Instructor> instructores)
        {
            return instructores.Select(i => new InstructorViewModel
            {
                Id = i.Id,
                NombreCompleto = $"{i.Nombre} {i.Apellido}",
            });
        }
    }
}
