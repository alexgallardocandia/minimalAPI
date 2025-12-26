namespace TechnicalTestApi.Application.Currencies.Command;

public class CreateCurrencyCommand
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal RateToBase { get; set; }
}