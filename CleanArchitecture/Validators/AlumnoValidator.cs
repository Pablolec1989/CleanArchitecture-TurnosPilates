using CleanArchitecture.Controllers;
using FluentValidation;
using InterfaceAdapter___Mappers.DTOs.Requests;

namespace CleanArchitecture.Validators
{
    public class AlumnoValidator : AbstractValidator<AlumnoRequestDTO>
    {
        public AlumnoValidator()
        {
            RuleFor(dto => dto.Nombre).NotEmpty()
                .WithMessage("El alumno debe tener nombre");
            RuleFor(dto => dto.Apellido).NotEmpty()
                .WithMessage("El alumno debe tener apellido");
            RuleFor(dto => dto.NroTelefono).NotEmpty()
                .WithMessage("El alumno debe tener Nro Telefonico");
        }
    }
}
