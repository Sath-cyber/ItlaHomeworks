using FluentValidation;
using Physio.Application.Dtos;

namespace Physio.Api.Validators
{
    public class SesionValidator : AbstractValidator<SesionDto>
    {
        public SesionValidator()
        {
            RuleFor(x => x.PacienteId)
                .GreaterThan(0).WithMessage("PacienteId es requerido.");

            RuleFor(x => x.FisioterapeutaId)
                .GreaterThan(0).WithMessage("FisioterapeutaId es requerido.");

            RuleFor(x => x.TratamientoId)
                .GreaterThan(0).WithMessage("TratamientoId es requerido.");

            RuleFor(x => x.FechaHora)
                .NotEmpty().WithMessage("La fecha y hora son requeridas.")
                .GreaterThan(DateTime.MinValue).WithMessage("La fecha y hora no son validas.");

            RuleFor(x => x.Notas)
                .MaximumLength(500).WithMessage("Las notas no deben superar los 500 caracteres.");
        }
    }
}
