using TechnicalTestApi.Application.Addresses.Command;
using TechnicalTestApi.Infraestructure;

namespace TechnicalTestApi.Handlers.Addresses;

public class DeleteAddressHandler
{
    private readonly AppDbContext _db;

    public DeleteAddressHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteAddressCommand command)
    {
        var address = await _db.Addresses.FindAsync(command.Id);
        if (address == null) return false;

        _db.Addresses.Remove(address);
        await _db.SaveChangesAsync();
        return true;
    }
}
