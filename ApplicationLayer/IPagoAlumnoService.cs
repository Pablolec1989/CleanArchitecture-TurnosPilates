using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public interface IPagoAlumnoService<T>
    {
        Task<PagoAlumno> GenerarOActualizarPagoParaAlumno(int alumnoId);
    }

}
