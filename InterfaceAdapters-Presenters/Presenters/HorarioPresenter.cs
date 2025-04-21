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
    public class HorarioPresenter : IPresenter<Horario, HorarioViewModel>
    {
        public IEnumerable<HorarioViewModel> Present(IEnumerable<Horario> horarios)
        {
            return horarios.Select(h => new HorarioViewModel
            {
                Horario = $"{h.Dia} {h.Hora}"
            });
        }
    }
}
