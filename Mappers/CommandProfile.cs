using AutoMapper;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Application.Users.Command;

namespace TechnicalTestApi.Mappers;

public class CommandProfile : Profile
{
    public CommandProfile()
    {
        // Mapeo de CreateUserCommand a User
        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore()) 
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Addresses, opt => opt.Ignore());
        
        // Mapeo de UpdateUserCommand a User
        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Addresses, opt => opt.Ignore());
    }
}