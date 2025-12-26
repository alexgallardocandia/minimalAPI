using FluentValidation;
using TechnicalTestApi.Application.Currencies.Command;

namespace TechnicalTestApi.Validators.Currencies;

public class ConvertCurrencyValidator : AbstractValidator<ConvertCurrencyCommand>
{
    public ConvertCurrencyValidator()
    {
        RuleFor(x => x.FromCurrencyCode)
            .NotEmpty().WithMessage("La moneda de origen es requerida")
            .Length(3).WithMessage("El código debe tener 3 caracteres");

        RuleFor(x => x.ToCurrencyCode)
            .NotEmpty().WithMessage("La moneda de destino es requerida")
            .Length(3).WithMessage("El código debe tener 3 caracteres")
            .NotEqual(x => x.FromCurrencyCode).WithMessage("Las monedas no pueden ser iguales");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a 0");
    }
}