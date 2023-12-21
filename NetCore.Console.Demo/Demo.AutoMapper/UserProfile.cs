using AutoMapper;
using Demo.AutoMapper.Dtos;
using Demo.AutoMapper.Models;

namespace Demo.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, UserModel>();
        ReplaceMemberName("Name", "UserName");
        ReplaceMemberName("Roles", "AssignRoles");
    }
}
