namespace TechnicalTestApi.Application.Users.Command;

public record UpdateUserCommand(
    int Id,
    string Name,
    string Email,
    bool IsActive,
    string? Password = null
);
