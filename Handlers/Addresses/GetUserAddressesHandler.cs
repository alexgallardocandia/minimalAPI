using Microsoft.EntityFrameworkCore;
using TechnicalTestApi.Application.Addresses.Queries;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Infraestructure;

namespace TechnicalTestApi.Handlers.Addresses;

public class GetUserAddressesHandler
{
    private readonly AppDbContext _db;

    public GetUserAddressesHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Address>> Handle(GetUserAddressesQuery query)
    {
        return await _db.Addresses
            .Where(a => a.UserId == query.UserId)
            .ToListAsync();
    }
}
