using System.ComponentModel.DataAnnotations;

namespace TechnicalTestApi.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public string Password { get; set; } = null!;

    public ICollection<Address> Addresses { get; set; } = new List<Address>();

}
