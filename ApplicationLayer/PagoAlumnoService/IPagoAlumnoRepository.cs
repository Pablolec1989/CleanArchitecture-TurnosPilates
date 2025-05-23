using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.PagoAlumnoService
{
    public interface IPagoAlumnoRepository
    {
        Task AddAsync(PagoAlumno pagoAlumno);
        Task<PagoAlumno?> GetByAlumnoIdAsync(int alumnoId);
        Task<IEnumerable<PagoAlumno>> GetAllAsync();
        Task UpdateAsync(PagoAlumno pagoAlumno);
        Task DeleteAsync(int alumnoId);
    }
}
