using FluentValidation;
using Physio.Application.Dtos;

namespace Physio.Api.Validators
{
    public class FisioterapeutaValidator : AbstractValidator<FisioterapeutaDto>
    {
        public FisioterapeutaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres.")
                .MaximumLength(80).WithMessage("El nombre no debe superar los 80 caracteres.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es requerido.")
                .MinimumLength(2).WithMessage("El apellido debe tener al menos 2 caracteres.")
                .MaximumLength(80).WithMessage("El apellido no debe superar los 80 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El telefono es requerido.")
                .Matches(@"^\d{10}$").WithMessage("El telefono debe tener exactamente 10 digitos.");

            RuleFor(x => x.Especialidad)
                .NotEmpty().WithMessage("La especialidad es requerida.")
                .MinimumLength(3).WithMessage("La especialidad debe tener al menos 3 caracteres.")
                .MaximumLength(100).WithMessage("La especialidad no debe superar los 100 caracteres.");
        }
    }
}
