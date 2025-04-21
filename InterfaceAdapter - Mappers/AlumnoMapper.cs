using ApplicationLayer;
using EnterpriseLayer_Entities;
using InterfaceAdapter___Mappers.DTOs.Requests;

namespace InterfaceAdapter___Mappers
{
    public class AlumnoMapper : IMapper<AlumnoRequestDTO, Alumno>
    {
        public Alumno ToEntity(AlumnoRequestDTO dto)
        {
            return new Alumno
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Observaciones = dto.Observaciones,
                NroTelefono = dto.NroTelefono,
            };
        }
    }
}
