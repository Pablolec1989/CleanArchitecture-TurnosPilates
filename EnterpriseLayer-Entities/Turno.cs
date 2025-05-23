using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLayer_Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public int HorarioId { get; set; }
        public int? InstructorId { get; set; }
        public int Capacidad { get; set; }
        public int Disponibilidad { get; set; }
        public List<TurnoAlumno>? Alumnos { get; set; }
        //Prop de navegacion
        public Horario? Horario { get; set; }
        public Instructor? Instructor { get; set; }

    }
}
