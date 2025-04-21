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
    public class InstructorMapper : IMapper<InstructorRequestDTO, Instructor>
    {
        public Instructor ToEntity(InstructorRequestDTO dto)
        {
            return new Instructor
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                NroTelefono = dto.NroTelefono,
                PorcentajeDePago = dto.PorcentajeDePago
            };
        }
    }
}
