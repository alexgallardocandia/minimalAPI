namespace TechnicalTestApi.Application.Users.Command;

public class CreateUserCommand
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}