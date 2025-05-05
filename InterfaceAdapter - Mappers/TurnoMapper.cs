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
    public class TurnoMapper : IMapper<TurnoRequestDTO, Turno>
    {
        public Turno ToEntity(TurnoRequestDTO dto)
        {
            var turno = new Turno
            {
                HorarioId = dto.HorarioId,
                InstructorId = dto.InstructorId,
                Alumnos = dto.Alumnos?.Select(alumnoId => new TurnosAlumnos
                {
                    AlumnoId = alumnoId

                }).ToList() ?? new List<TurnosAlumnos>(),

                CapacidadMaxima = dto.Capacidad
            };
            return turno;
        }
    }
}
