using ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapter___Mappers.DTOs.Requests
{
    public class TurnoRequestDTO
    {
        public int HorarioId { get; set; }
        public int? InstructorId { get; set; }
        public List<int>? Alumnos { get; set; }
        public int Capacidad { get; set; }
    }
}
