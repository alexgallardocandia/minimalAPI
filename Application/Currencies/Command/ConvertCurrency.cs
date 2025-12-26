namespace TechnicalTestApi.Application.Currencies.Command;

public class ConvertCurrencyCommand
{
    public string FromCurrencyCode { get; set; } = null!;
    public string ToCurrencyCode { get; set; } = null!;
    public decimal Amount { get; set; }
}