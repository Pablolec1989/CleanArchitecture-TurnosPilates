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
    public class AlumnoRequestMapper : IMapper<AlumnoRequestDTO, Alumno>
    {
        public Alumno ToEntity(AlumnoRequestDTO dto)
        {
            return new Alumno
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Observaciones = dto.Observaciones,
                NroTelefono = dto.NroTelefono
            };

        }
    }
}
