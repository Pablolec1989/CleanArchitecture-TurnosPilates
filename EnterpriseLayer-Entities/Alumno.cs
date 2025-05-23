﻿using EnterpriseLayer_Entities;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseLayer_Entities
{
    public class Alumno
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string? Observaciones { get; set; }
        public required string NroTelefono { get; set; }
        public List<TurnoAlumno>? Turnos { get; set; }

    }
}