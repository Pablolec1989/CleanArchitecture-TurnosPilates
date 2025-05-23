using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public interface ITurnoAlumnoRepository
    {
        Task AddAlumnosToTurnoAsync(int turnoId, IEnumerable<int> alumnoIds);
        Task UpdateAlumnosToTurnoAsync(int turnoId, IEnumerable<int> alumnoIds);
        Task DeleteAlumnoEnTurnoAsync(int turnoId, IEnumerable<int> alumnoIds);
    }
}
