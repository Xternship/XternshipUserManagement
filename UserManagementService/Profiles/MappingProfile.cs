using AutoMapper;
using UserManagementService.Data.Entities;
using UserManagementService.Dtos;

namespace UserManagementService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); 
           
            CreateMap<User, UserDto>();
        }
    }
}