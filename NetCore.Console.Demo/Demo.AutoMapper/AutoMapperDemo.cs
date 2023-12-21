using AutoMapper;
using Demo.AutoMapper.Dtos;
using Demo.AutoMapper.Models;

namespace Demo.AutoMapper;

//https://docs.automapper.org/en/latest/
//https://docs.automapper.org/en/latest/Getting-started.html
public class AutoMapperDemo
{
    public static void Run()
    {
        TestMapperConfiguration();
        TestProfile();
    }

    private static void TestMapperConfiguration()
    {
        Console.WriteLine("TestMapperConfiguration:");
        var userDto = GetUserDto();
        var nodeDto = GetNodeDto();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserDto, UserModel>();
            cfg.CreateMap<NodeDto, NodeModel>();
            cfg.ReplaceMemberName("Name", "UserName");
            cfg.ReplaceMemberName("Name", "NodeName");
            cfg.ReplaceMemberName("Roles", "AssignRoles");
        });
        var mapper = config.CreateMapper();

        var userModel = mapper.Map<UserModel>(userDto);
        var nodeModel = mapper.Map<NodeModel>(nodeDto);
    }

    private static void TestProfile()
    {
        var userDto = GetUserDto();
        var nodeDto = GetNodeDto();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<NodeProfile>();
        });

        var mapper = config.CreateMapper();

        var userModel = mapper.Map<UserModel>(userDto);
        var nodeModel = mapper.Map<NodeModel>(nodeDto);
    }

    private static UserDto GetUserDto() 
    {
        var userDto = new UserDto()
        {
            Id = 1,
            PassWord = "123",
            Name = "User1",
            Roles = new()
            {
                "Admin",
                "Guest",
                "User"
            }
        };
        return userDto;
    }

    private static NodeDto GetNodeDto()
    {
        var nodeDto = new NodeDto()
        {
            Id = "1",
            Name = "node1",
            SubNodes = new()
            {
                new NodeDto() {Id = "1-1",Name="subNode1-1"},
                new NodeDto() {Id = "1-2",Name="subNode1-2"},
                new NodeDto()
                {
                    Id = "1-3",Name="subNode1-3",SubNodes = new(){ new NodeDto() { Id = "1-3-1", Name = "subNode1-3-1" } }
                }
            }
        };
        return nodeDto;
    }

}
