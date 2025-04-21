using FluentValidation;
using InterfaceAdapter___Mappers;
using InterfaceAdapter___Mappers.DTOs.Requests;

namespace CleanArchitecture.Validators
{
    public class InstructorValidator : AbstractValidator<InstructorRequestDTO>
    {
        public InstructorValidator()
        {
            RuleFor(dto => dto.Nombre).NotEmpty()
                .WithMessage("El campo nombre es requerido");
            RuleFor(dto => dto.Apellido).NotEmpty()
                .WithMessage("El campo apellido es requerido");
            RuleFor(dto => dto.NroTelefono).NotEmpty()
                .WithMessage("El Numero de telefono es requerido");
            RuleFor(dto => dto.PorcentajeDePago).NotEmpty().
                WithMessage("El porcentaje de pago es requerido");
        }
    }
}
