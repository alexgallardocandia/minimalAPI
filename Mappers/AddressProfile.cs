using AutoMapper;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Dtos;
using TechnicalTestApi.Application.Addresses.Command;

namespace TechnicalTestApi.Mappers;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        // DTOs a Commands
        CreateMap<CreateAddressDto, CreateAddressCommand>();
        CreateMap<UpdateAddressDto, UpdateAddressCommand>();
        
        // Commands a Entities
        CreateMap<CreateAddressCommand, Address>();
        CreateMap<UpdateAddressCommand, Address>();
        
        // Entities a DTOs
        CreateMap<Address, AddressDto>();
    }
}