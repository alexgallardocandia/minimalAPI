namespace TechnicalTestApi.Dtos;

public class CurrencyDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal RateToBase { get; set; }
    public DateTime CreatedAt { get; set; }
}