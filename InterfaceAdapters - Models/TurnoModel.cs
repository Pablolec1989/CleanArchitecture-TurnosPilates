using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters___Models
{
    public class TurnoModel
    {
        public int Id { get; set; }
        public int HorarioId { get; set; }
        public int? InstructorId { get; set; }
        public int Capacidad { get; set; }
        public List<TurnoAlumnoModel>? Alumnos { get; set; }

        // Propiedades de navegación
        [ForeignKey("HorarioId")]
        public HorarioModel? Horario { get; set; }
        [ForeignKey("InstructorId")]
        public InstructorModel? Instructor { get; set; }


    }
}
