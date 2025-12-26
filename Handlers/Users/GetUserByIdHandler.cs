using TechnicalTestApi.Application.Users.Queries;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Infraestructure;
using TechnicalTestApi.Mappers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TechnicalTestApi.Dtos;

public class GetUserByIdHandler
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery query)
    {
        var user = await _context.Users
            .Include(u => u.Addresses)
            .FirstOrDefaultAsync(u => u.Id == query.Id);

        if (user is null) return null;

        // Mapear usando AutoMapper
        return _mapper.Map<UserDto>(user);
    }
}