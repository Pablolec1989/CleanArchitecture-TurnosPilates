using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLayer_Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string NroTelefono { get; set; }
        public required int PorcentajeDePago { get; set; }
    }
}
