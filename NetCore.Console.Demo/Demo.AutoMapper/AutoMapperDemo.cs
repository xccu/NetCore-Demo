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

        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<NodeDto, NodeModel>());
        var mapper2 = config.CreateMapper();
        var model2 = mapper.Map<NodeModel>(nodeDto);


    }
}
