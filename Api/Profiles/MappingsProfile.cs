using AutoMapper;
using ApiGateway.Dtos;
using ApiGateway.Models;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        // Map RegisterUserDto to UserProfile
        CreateMap<RegisterUserDto, UserProfile>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));


        // Map UserProfile to ApiResponse if needed
         CreateMap<UserProfile, ApiResponse>()
             .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true))
             .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "User profile successfully mapped"));
    }
}
