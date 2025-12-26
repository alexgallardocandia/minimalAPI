using AutoMapper;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Dtos;
using TechnicalTestApi.Application.Currencies.Command;

namespace TechnicalTestApi.Mappers;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        // DTOs a Commands
        CreateMap<CreateCurrencyDto, CreateCurrencyCommand>();
        CreateMap<CurrencyConversionRequestDto, ConvertCurrencyCommand>();
        
        // Commands a Entities
        CreateMap<CreateCurrencyCommand, Currency>();
        
        // Entities a DTOs
        CreateMap<Currency, CurrencyDto>();
        
        // Para la respuesta de conversión
        CreateMap<(decimal convertedAmount, Currency from, Currency to), CurrencyConversionResponseDto>()
            .ConstructUsing(src => new CurrencyConversionResponseDto
            {
                FromCurrency = src.from.Code,
                ToCurrency = src.to.Code,
                OriginalAmount = 0, // Se llena después
                ConvertedAmount = src.convertedAmount,
                Rate = src.to.RateToBase / src.from.RateToBase,
                ConversionDate = DateTime.UtcNow
            });
    }
}