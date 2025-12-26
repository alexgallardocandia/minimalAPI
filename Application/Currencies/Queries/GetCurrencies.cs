namespace TechnicalTestApi.Application.Currencies.Queries;

public class GetCurrenciesQuery
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}