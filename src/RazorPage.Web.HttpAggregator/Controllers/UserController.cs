using Microsoft.AspNetCore.Mvc;
using RazorPage.Web.HttpAggregator.Services.Interfaces;
using User.ApplicationCore.Dtos;

namespace RazorPage.Web.HttpAggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IHttpUserService _service;

    public UserController(ILogger<UserController> logger, IHttpUserService service)
    {
        _logger = logger;
        _service = service;

    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var result =  await _service.GetAllAsync();
        return result;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDto> GetUser(string id)
    {
        var result = await _service.GetAsync(id);
        return result;
    }


    [HttpPost]
    [Route("create")]
    public async Task<bool> CreateAsync(UserDto user)
    {
        return await _service.CreateAsync(user);
    }

    [HttpPut]
    [Route("update")]
    public async Task<bool> UpdateUser(UserDto user)
    {
        return await _service.UpdateAsync(user);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<bool> DeleteUser(string id)
    {
        return await _service.DeleteAsync(id);
    }

}
