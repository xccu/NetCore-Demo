using AutoMapper;

namespace Demo.AutoMapper;

//https://docs.automapper.org/en/latest/
public class AutoMapperDemo
{


    public static void Run()
    {
        var dto = new UserDto()
        {
            Id = "1",
            PassWord = "123",
            UserName = "User1",
            AssignRoles = new()
            {
                "Admin",
                "Guest",
                "User"
            }
        };

        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, UserModel>());
        var mapper = config.CreateMapper();
        var model =  mapper.Map<UserModel>(dto);
    }
}
