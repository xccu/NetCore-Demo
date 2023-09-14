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
    public User GetByName(string name)
    {
        var result = _service.GetByName(name);
        return result;
    }

    [HttpGet("race/{race}")]
    public IEnumerable<User> GetByRace(string race)
    {
        var result = _service.GetByRace(race);
        return result;
    }

    [HttpPost("create")]
    public int CreateUser(User user)
    {
        var result = _service.Create(user);
        return result;
    }

    [HttpPut("update")]
    public int UpdateUser(User user)
    {
        var result = _service.Update(user);
        return result;
    }


    [HttpDelete("delete/{id}")]
    public int DeleteUser(int id)
    {
        var result = _service.Delete(id);
        return result;
    }

    [HttpDelete("delete")]
    public int BatchDelete()
    {
        var result = _service.BatchDelete();
        return result;
    }

    [HttpPost("split")]
    public void Split()
    {
        _service.Split();
    }
}

