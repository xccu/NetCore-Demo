using AutoMapper;
using User.ApplicationCore.Dtos;

namespace User.ApplicationCore.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Entities.User, UserDto>();
        CreateMap<UserDto, Entities.User>();
    }
}
