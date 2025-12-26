namespace TechnicalTestApi.Dtos;

public class UpdateAddressDto
{
    public int Id { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
}