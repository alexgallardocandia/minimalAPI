using TechnicalTestApi.Application.Users.Command;
using TechnicalTestApi.Infraestructure;
using Microsoft.AspNetCore.Identity;
using TechnicalTestApi.Domain.Entities;

public class UpdateUserHandler
{
    private readonly AppDbContext _db;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UpdateUserHandler(AppDbContext db, IPasswordHasher<User> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> Handle(UpdateUserCommand command)
    {
        var user = await _db.Users.FindAsync(command.Id);
        if (user == null) return false;

        user.Name = command.Name;
        user.Email = command.Email;
        user.IsActive = command.IsActive;

        if (!string.IsNullOrEmpty(command.Password))
        {
            user.Password = _passwordHasher.HashPassword(user, command.Password);
        }

        await _db.SaveChangesAsync();
        return true;
    }
}
