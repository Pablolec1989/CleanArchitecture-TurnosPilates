using FluentValidation;
using InterfaceAdapter___Mappers.DTOs.Requests;

namespace CleanArchitecture.Validators
{
    public class TarifaValidator : AbstractValidator<TarifaRequestDTO>
    {
        public TarifaValidator()
        {
            RuleFor(dto => dto.Frecuencia_turno).NotEmpty()
                .WithMessage("La tarifa debe tener frecuencia de turno")
                .GreaterThan(0);

            RuleFor(dto => dto.Precio).NotEmpty()
                .WithMessage("La tarifa debe tener un precio")
                .GreaterThan(0);
        }

    }
}
