using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLayer_Entities
{
    public class Tarifa
    {
        public int Id { get; set; }
        public int Frecuencia_turno { get; set; }
        public decimal Precio { get; set; }
    }
}
