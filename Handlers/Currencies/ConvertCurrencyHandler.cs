using Microsoft.EntityFrameworkCore;
using TechnicalTestApi.Application.Currencies.Command;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Infraestructure;

namespace TechnicalTestApi.Handlers.Currencies;

public class ConvertCurrencyHandler
{
    private readonly AppDbContext _context;

    public ConvertCurrencyHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(decimal convertedAmount, Currency from, Currency to)> Handle(ConvertCurrencyCommand command)
    {
        var fromCurrency = await _context.Currencies
            .FirstOrDefaultAsync(c => c.Code == command.FromCurrencyCode);
        
        var toCurrency = await _context.Currencies
            .FirstOrDefaultAsync(c => c.Code == command.ToCurrencyCode);

        if (fromCurrency is null)
            throw new KeyNotFoundException($"Moneda no encontrada: {command.FromCurrencyCode}");
        
        if (toCurrency is null)
            throw new KeyNotFoundException($"Moneda no encontrada: {command.ToCurrencyCode}");

        // Fórmula CORRECTA de conversión:
        // convertedAmount = amount * (to.RateToBase / from.RateToBase)
        var rate = toCurrency.RateToBase / fromCurrency.RateToBase;
        var convertedAmount = command.Amount * rate;

        return (convertedAmount, fromCurrency, toCurrency);
    }
}