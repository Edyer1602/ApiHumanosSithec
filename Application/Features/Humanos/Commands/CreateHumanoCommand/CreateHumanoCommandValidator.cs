using FluentValidation;

namespace Application.Features.Humanos.Commands.CreateHumanoCommand
{
    public class CreateHumanoCommandValidator : AbstractValidator<CreateHumanoCommand>
    {
        private const string Masculino = "M";
        private const string Femenino = "F";
        public CreateHumanoCommandValidator()
        {
            List<string> sexos = new() { Masculino, Femenino };
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
                .MaximumLength(150).WithMessage("{PropertyName} no puede exceder de {MaxLength} caracteres");

            RuleFor(x => x.Sexo.ToString())
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
                .Must(x=> sexos.Contains(x.ToUpper())).WithMessage($"Por favor solo use {Masculino} (Masculino) o {Femenino} (Femenino) para especificar el sexo");

            RuleFor(x => x.Edad)
                .InclusiveBetween(1, 99).WithMessage("Solo se permite una {PropertyName} de {From} a {To}");

            RuleFor(x => x.Peso)
                .GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0");

            RuleFor(x => x.Altura)
                .GreaterThan(0).WithMessage("La {PropertyName} debe ser mayor a 0");
        }
    }
}
