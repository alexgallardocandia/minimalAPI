using FluentValidation;
using TechnicalTestApi.Application.Addresses.Command;
using TechnicalTestApi.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTestApi.Validators.Addresses;

public class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
{
    private readonly AppDbContext db;
    
    public CreateAddressValidator(AppDbContext context)
    {
        db = context;
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
    }
}
