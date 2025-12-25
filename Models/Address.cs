using System.ComponentModel.DataAnnotations;

namespace TechnicalTestApi.Models;

public class Address
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Street { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;

    public string? ZipCode { get; set; }

    // Navegación
    public User? User { get; set; }
}
