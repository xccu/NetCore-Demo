using AutoMapper;

namespace Demo.AutoMapper;

//https://docs.automapper.org/en/latest/
//https://docs.automapper.org/en/latest/Getting-started.html
public class AutoMapperDemo
{


    public static void Run()
    {
        var userDto = new UserDto()
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

        var nodeDto = new NodeDto()
        {
            Id = 1,
            Name = "node1",
            SubNodes = new()
            {
                new NodeDto() {Id = 2,Name="subNode1-2"},
                new NodeDto() {Id = 3,Name="subNode1-3"},
                new NodeDto()
                {
                    Id = 4,Name="subNode1-4",SubNodes = new(){ new NodeDto() { Id = 2, Name = "subNode1-4-5" } }
                }
            }
        };

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserDto, UserModel>();
            cfg.CreateMap<NodeDto, NodeModel>();
        });
        var mapper = config.CreateMapper();

        var userModel = mapper.Map<UserModel>(userDto);
        var nodeModel = mapper.Map<NodeModel>(nodeDto);

    }

}
