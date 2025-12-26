using FluentValidation;
using TechnicalTestApi.Application.Currencies.Command;

namespace TechnicalTestApi.Validators.Currencies;

public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyCommand>
{
    public CreateCurrencyValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código de la moneda es requerido")
            .Length(3).WithMessage("El código debe tener 3 caracteres")
            .Matches("^[A-Z]{3}$").WithMessage("El código debe contener solo letras mayúsculas");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la moneda es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.RateToBase)
            .GreaterThan(0).WithMessage("La tasa debe ser mayor a 0");
    }
}
