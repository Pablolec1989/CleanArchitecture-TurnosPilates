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
    public class TarifaRequestMapper : IMapper<TarifaRequestDTO, Tarifa>
    {
        public Tarifa ToEntity(TarifaRequestDTO dto)
        {
            return new Tarifa
            {
                Frecuencia_turno = dto.Frecuencia_turno,
                Precio = dto.Precio
            };
        }
    }
}
