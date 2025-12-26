using TechnicalTestApi.Application.Users.Command;
using TechnicalTestApi.Infraestructure;

public class DeleteUserHandler
{
    private readonly AppDbContext _db;

    public DeleteUserHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteUserCommand command)
    {
        var user = await _db.Users.FindAsync(command.Id);
        if (user == null) return false;

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }
}
