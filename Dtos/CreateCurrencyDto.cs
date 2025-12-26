namespace TechnicalTestApi.Dtos;

public class CreateCurrencyDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal RateToBase { get; set; }
}