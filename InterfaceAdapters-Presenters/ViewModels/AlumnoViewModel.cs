using EnterpriseLayer_Entities;

namespace InterfaceAdapters_Presenters.ViewModels
{
    public class AlumnoViewModel
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public List<TurnoAlumno> Turnos { get; set; }
    }
}
