
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using User.ApplicationCore.Dtos;
using User.ApplicationCore.Entities;
using User.ApplicationCore.Interfaces.Services;
using Entities = User.ApplicationCore.Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserController(ILogger<UserController> logger, IUserService service,IMapper mapper)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Entities.User> GetAll()
    {
        var user = _service.GetUsers().ToList();
        //var dto = _mapper.Map<UserDto>(user);
        return user;
    }

    [HttpGet("{id}")]
    public UserDto GetUser(string id)
    {
        var user = _service.GetUser(id);
        var dto = _mapper.Map<UserDto>(user);
        return dto;
    }

    [HttpGet("query")]
    public IEnumerable<Entities.User> GetByQuery()
    {
        //var user = _context.User.FromSqlRaw($"select * from user where id in(1,2)");
        Expression<Func<Entities.User, bool>> express = a => a.gender == "male";
        var user = _service.SearchCondition(express);
        return user.ToList();
    }

    [HttpGet("name/{name}")]
    public Entities.User GetUserByName(String name)
    {
        Expression<Func<Entities.User, bool>> express = a => a.name == name;
        var user = _service.SearchCondition(express);
        return user.FirstOrDefault();
    }

    [HttpPut]
    public bool UpdateUser(UserDto dto)
    {
        var user = _mapper.Map<Entities.User>(dto);
        return _service.Update(user);
    }

    [HttpPost]
    [Route("insert")]
    public bool CreateUser()
    {
        var user = new Entities.User();
        user.name = "test";
        user.gender = "male";
        user.password = "123";
        user.age = 100;
        user.race = "unknown";
        return _service.Insert(user);
    }

    [HttpPost]
    public async Task<bool> CreateUserAsync(UserDto dto)
    {
        var user = _mapper.Map<Entities.User>(dto);
        return await _service.InsertAsync(user);
    }

    [HttpDelete("id")]
    public bool DeleteUser(string id)
    {
        var user = _service.GetUser(id);
        return _service.Delete(user);
    }

}
