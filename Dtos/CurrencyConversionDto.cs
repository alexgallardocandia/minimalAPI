namespace TechnicalTestApi.Dtos;

public class CurrencyConversionRequestDto
{
    public string FromCurrencyCode { get; set; } = null!;
    public string ToCurrencyCode { get; set; } = null!;
    public decimal Amount { get; set; }
}

public class CurrencyConversionResponseDto
{
    public string FromCurrency { get; set; } = null!;
    public string ToCurrency { get; set; } = null!;
    public decimal OriginalAmount { get; set; }
    public decimal ConvertedAmount { get; set; }
    public decimal Rate { get; set; }
    public DateTime ConversionDate { get; set; }
}