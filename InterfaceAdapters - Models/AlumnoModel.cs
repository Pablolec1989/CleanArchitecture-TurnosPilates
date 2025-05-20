using EnterpriseLayer_Entities;

namespace InterfaceAdapters___Models
{
    public class AlumnoModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string? Observaciones { get; set; }
        public required string NroTelefono { get; set; }
        public List<TurnoAlumnoModel>? Turnos { get; set; }


    }
}
