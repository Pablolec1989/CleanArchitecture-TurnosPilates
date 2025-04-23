using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLayer_Entities
{
    public class Horario
    {
        public int Id { get; set; }
        public required string Dia { get; set; }
        public required string Hora { get; set; }
        public List<Turno>? Turnos { get; set; }
    }
}
