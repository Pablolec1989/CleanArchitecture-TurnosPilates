using FluentValidation;
using InterfaceAdapter___Mappers.DTOs.Requests;

namespace CleanArchitecture.Validators
{
    public class TurnoValidator : AbstractValidator<TurnoRequestDTO>
    {
        public TurnoValidator()
        {
            RuleFor(dto => dto.Capacidad).NotEmpty()
                .WithMessage("La capacidad del turno no puede estar vacía");
        }
    }
}
