using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters___Models
{
    public class PagoAlumnoModel
    {
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        public int TarifaId { get; set; }
        public Tarifa? Tarifa { get; set; }
        public decimal Monto { get; set; }
        public bool Pagado { get; set; } = false;
    }
}
