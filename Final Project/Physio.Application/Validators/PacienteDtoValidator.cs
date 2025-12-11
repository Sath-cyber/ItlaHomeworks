using FluentValidation;
using Physio.Application.Dtos;

namespace Physio.Application.Validators
{
    public class PacienteValidator : AbstractValidator<PacienteDto>
    {
        public PacienteValidator()
        {
            RuleFor(x => x.Id)
                .Equal(0).WithMessage("El Id no debe enviarse al crear un paciente.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El campo Nombre es requerido.")
                .MaximumLength(50).WithMessage("El Nombre no puede tener más de 50 caracteres.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El campo Apellido es requerido.")
                .MaximumLength(50).WithMessage("El Apellido no puede tener más de 50 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El campo Telefono es requerido.")
                .Matches(@"^[0-9]{10}$").WithMessage("El Telefono debe ser un número válido de 10 dígitos.");

            RuleFor(x => x.Edad)
                .InclusiveBetween(1, 120)
                .WithMessage("El campo Edad debe estar entre 1 y 120.");
        }
    }
}
