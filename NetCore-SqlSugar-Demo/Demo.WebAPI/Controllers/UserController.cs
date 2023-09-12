using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Entities;

namespace Demo.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _service;
    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<User> GetAll()
    {
        var result = _service.GetAll();
        return result;
    }

    [HttpGet("name/{name}")]
    public User GetAll(string name)
    {
        var result = _service.GetByName(name);
        return result;
    }

    [HttpPost("create")]
    public int CreateUser(User user)
    {
        var result = _service.CreateUser(user);
        return result;
    }
}

