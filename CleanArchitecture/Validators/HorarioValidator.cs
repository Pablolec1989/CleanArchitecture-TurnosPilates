using FluentValidation;
using InterfaceAdapter___Mappers.DTOs.Requests;

namespace CleanArchitecture.Validators
{
    public class HorarioValidator : AbstractValidator<HorarioRequestDTO>
    {
        public HorarioValidator()
        {
            RuleFor(dto => dto.Dia).NotEmpty().
                WithMessage("El dia no debe estar vacío");

            RuleFor(dto => dto.Hora).NotEmpty().
                WithMessage("La hora no debe estar vacía");
        }
    }
}
