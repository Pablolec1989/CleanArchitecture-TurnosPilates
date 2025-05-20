using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters_Presenters.ViewModels
{
    public class TurnoViewModel
    {
        public int Id { get; set; }
        //Prop de navegacion
        public HorarioViewModel? Horario { get; set; }
        public InstructorViewModel? Instructor { get; set; }
        public List<AlumnoViewModel>? Alumnos { get; set; }
        public int Capacidad { get; set; }
        public int Disponibilidad { get; set; }
    }
}
