using TechnicalTestApi.Application.Users.Queries;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Infraestructure;
using TechnicalTestApi.Mappers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TechnicalTestApi.Dtos;

public class GetUsersHandler
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery query)
    {
        var queryable = _context.Users
            .Include(u => u.Addresses)
            .AsQueryable();

        if (query.IsActive.HasValue)
        {
            queryable = queryable.Where(u => u.IsActive == query.IsActive.Value);
        }

        var users = await queryable.ToListAsync();
        
        
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}