using ApplicationLayer;
using EnterpriseLayer_Entities;
using InterfaceAdapter___Mappers.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapter___Mappers
{
    public class HorarioMapper : IMapper<HorarioRequestDTO, Horario>
    {
        public Horario ToEntity (HorarioRequestDTO dto)
        {
            return new Horario
            {
                Id = dto.Id,
                Dia = dto.Dia,
                Hora = dto.Hora
            };
        }
    }
}
