using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechnicalTestApi.Dtos;
using TechnicalTestApi.Application.Currencies.Queries;
using TechnicalTestApi.Infraestructure;

namespace TechnicalTestApi.Handlers.Currencies;

public class GetCurrenciesHandler
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetCurrenciesHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CurrencyDto>> Handle(GetCurrenciesQuery query)
    {
        var currencies = await _context.Currencies
            .OrderBy(c => c.Code)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
    }
}