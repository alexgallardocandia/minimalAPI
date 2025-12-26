using FluentValidation;
using TechnicalTestApi.Application.Users.Command;
using TechnicalTestApi.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTestApi.Validators.Users;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    private readonly AppDbContext db;
    
    public CreateUserValidator(AppDbContext context)
    {
        db = context;
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (email, ct) =>
                !await db.Users.AnyAsync(u => u.Email == email, ct)
            )
            .WithMessage("El email ya se encuentra registrado");
    }
}
