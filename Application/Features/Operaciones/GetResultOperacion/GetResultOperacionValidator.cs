using FluentValidation;

namespace Application.Features.Operaciones.GetResultOperacion
{
    public class GetResultOperacionValidator : AbstractValidator<GetResultOperacionQuery>
    {
        public GetResultOperacionValidator()
        {
            List<string> lstOperadores = new() { "*", "+", "-", "/" };
            RuleFor(x => x.operacion.ToLower())
                .Must(x => lstOperadores.Contains(x))
                .WithMessage("Solo se admiten los siguientes operadores +, -, *, /")
                .When(x => !string.IsNullOrEmpty(x.operacion));

            RuleFor(x => x.argumento2)
                .GreaterThan(0).When(y => y.operacion == "*")
                .WithMessage("El divisor no puede ser menor o igual a 0");
        }
    }
}
