using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLayer_Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public int? InstructorId { get; set; }
        public int HorarioId { get; set; }
        public int CapacidadMaxima { get; set; }

        //Prop de navegacion
        public Instructor? Instructor { get; set; }
        public List<TurnosAlumnos>? TurnoAlumnos { get; set; } = [];
        public Horario? Horario { get; set; }

    }
}
