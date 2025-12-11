using FluentValidation;
using Physio.Application.Dtos;

namespace Physio.Api.Validators
{
    public class TratamientoValidator : AbstractValidator<TratamientoDto>
    {
        public TratamientoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del tratamiento es requerido.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .MaximumLength(100).WithMessage("El nombre no debe superar los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripcion es requerida.")
                .MaximumLength(500).WithMessage("La descripcion no debe superar los 500 caracteres.");

            RuleFor(x => x.Costo)
                .GreaterThan(0).WithMessage("El costo debe ser mayor que 0.")
                .LessThanOrEqualTo(100000).WithMessage("El costo es demasiado alto.");

            RuleFor(x => x.DuracionMinutos)
                .GreaterThan(0).WithMessage("La duracion debe ser mayor que 0.")
                .LessThanOrEqualTo(480).WithMessage("La duracion no debe ser mayor que 480 minutos.");
        }
    }
}
