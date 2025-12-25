using System.ComponentModel.DataAnnotations;

namespace TechnicalTestApi.Models;

public class User
{
    public int Id { get; set; } // PK autoincrement por defecto

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    [Required]
    public string Password { get; set; } = string.Empty;

    // Relación 1:N
    public List<Address> Addresses { get; set; } = new();
}
