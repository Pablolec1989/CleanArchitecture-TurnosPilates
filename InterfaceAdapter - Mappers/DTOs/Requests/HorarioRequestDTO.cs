using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapter___Mappers.DTOs.Requests
{
    public class HorarioRequestDTO
    {
        public int Id { get; set; }
        public required string Dia { get; set; }
        public required string Hora { get; set; }
    }
}
