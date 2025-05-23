using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLayer_Entities
{
    public class TurnoAlumno
    {
        public int AlumnoId { get; set; }
        public int TurnoId { get; set; }

        // Propiedades de navegación
        public Alumno? Alumno { get; set; }
        public Turno? Turno { get; set; }
    }
}
