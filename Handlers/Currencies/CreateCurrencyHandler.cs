using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechnicalTestApi.Dtos;
using TechnicalTestApi.Application.Currencies.Command;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Infraestructure;

namespace TechnicalTestApi.Handlers.Currencies;

public class CreateCurrencyHandler
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CreateCurrencyHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CurrencyDto> Handle(CreateCurrencyCommand command)
    {
        // Verificar que el código sea único
        var exists = await _context.Currencies
            .AnyAsync(c => c.Code == command.Code);
        
        if (exists)
            throw new InvalidOperationException($"La moneda con código {command.Code} ya existe");

        var currency = _mapper.Map<Currency>(command);
        
        _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();

        return _mapper.Map<CurrencyDto>(currency);
    }
}
