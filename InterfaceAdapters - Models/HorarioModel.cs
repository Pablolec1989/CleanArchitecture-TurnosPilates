using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters___Models
{
    public class HorarioModel
    {
        public int Id { get; set; }
        public required string Dia { get; set; }
        public required string Hora { get; set; }
        public List<TurnoModel>? Turnos { get; set; } = [];
    }
}
