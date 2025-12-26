using TechnicalTestApi.Application.Users.Command;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Infraestructure;
using TechnicalTestApi.Mappers;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using TechnicalTestApi.Dtos;

namespace TechnicalTestApi.Handlers.Users;

public class CreateUserHandler
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;

    public CreateUserHandler(
        AppDbContext context, 
        IPasswordHasher<User> passwordHasher,
        IMapper mapper)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand command)
    {
        // Mapear Command a Entity usando AutoMapper
        var user = _mapper.Map<User>(command);
        
        // Hashear la contraseña (esto NO se hace con AutoMapper)
        user.Password = _passwordHasher.HashPassword(user, command.Password);
        user.IsActive = true;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Mapear Entity a DTO usando AutoMapper
        return _mapper.Map<UserDto>(user);
    }
}