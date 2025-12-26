using AutoMapper;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Dtos;
using TechnicalTestApi.Application.Users.Command;
using TechnicalTestApi.Application.Addresses.Command;

namespace TechnicalTestApi.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Mapeo de CreateUserDto a CreateUserCommand
        CreateMap<CreateUserDto, CreateUserCommand>();
        
        // Mapeo de UpdateUserDto a UpdateUserCommand
        CreateMap<UpdateUserDto, UpdateUserCommand>();
        
        // Mapeo de CreateAddressDto a CreateAddressCommand
        CreateMap<CreateAddressDto, CreateAddressCommand>();
        
        // Mapeo de User a UserDto (incluye Addresses)
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Addresses, 
                       opt => opt.MapFrom(src => src.Addresses));
        
        // Mapeo de Address a AddressDto
        CreateMap<Address, AddressDto>();
    }
}
