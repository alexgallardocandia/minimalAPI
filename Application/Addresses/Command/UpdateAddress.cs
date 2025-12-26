namespace TechnicalTestApi.Application.Addresses.Command;

public record UpdateAddressCommand(
    int Id,
    string Street,
    string City,
    string Country,
    string? ZipCode
);
