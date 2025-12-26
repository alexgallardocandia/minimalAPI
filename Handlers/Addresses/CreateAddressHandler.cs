using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechnicalTestApi.Application.Addresses.Command;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Dtos;
using TechnicalTestApi.Infraestructure;

public class CreateAddressHandler
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CreateAddressHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddressDto?> Handle(CreateAddressCommand command)
    {
        // Verificar que el usuario existe
        var userExists = await _context.Users.AnyAsync(u => u.Id == command.UserId);
        if (!userExists) return null;

        // Mapear Command a Entity usando AutoMapper
        var address = _mapper.Map<Address>(command);
        
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        // Mapear Entity a DTO usando AutoMapper
        return _mapper.Map<AddressDto>(address);
    }
}