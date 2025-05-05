using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters___Models
{
    public class TurnoAlumnoModel
    {
        public int AlumnoId { get; set; }
        public int TurnoId { get; set; }

        // Propiedades de navegación
        public AlumnoModel? Alumno { get; set; }
        public TurnoModel? Turno { get; set; }
    }
}
