namespace TechnicalTestApi.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; }
    public List<AddressDto> Addresses { get; set; } = new();
}