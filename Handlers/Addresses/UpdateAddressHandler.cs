using TechnicalTestApi.Application.Addresses.Command;
using TechnicalTestApi.Infraestructure;

namespace TechnicalTestApi.Handlers.Addresses;

public class UpdateAddressHandler
{
    private readonly AppDbContext _db;

    public UpdateAddressHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(UpdateAddressCommand command)
    {
        var address = await _db.Addresses.FindAsync(command.Id);
        if (address == null) return false;

        address.Street = command.Street;
        address.City = command.City;
        address.Country = command.Country;
        address.ZipCode = command.ZipCode;

        await _db.SaveChangesAsync();
        return true;
    }
}
