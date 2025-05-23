using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLayer_Entities
{
    public class PagoAlumno
    {
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        public int TarifaId { get; set; }
        public Tarifa? Tarifa { get; set; }
        public decimal Monto { get; set; }
        public bool Pagado { get; set; }
    }
}
