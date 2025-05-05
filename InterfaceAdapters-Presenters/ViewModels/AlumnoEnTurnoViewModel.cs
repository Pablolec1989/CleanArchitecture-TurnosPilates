using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters_Presenters.ViewModels
{
    public class AlumnoEnTurnoViewModel
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public List<string>? HorariosTurnos { get; set; }
    }
}
