using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Common.Custom;
using Common.Custom.Attributes;
using Common.Model;

namespace WebAPI.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private static List<User> _users;
    private UserDbContext _context;

    public UserController(ILogger<UserController> logger, UserDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("GetAll")]
    public IEnumerable<User> GetAll()
    {
        return _context.User.ToList();
    }

    [HttpGet("{name}")]
    public User GetByName(string name)
    {
        return _context.User.FirstOrDefault(t => t.name == name);
    }

    [HttpPost("Add")]
    [Validation]
    public IActionResult Add([FromBody]User user)
    {
        try
        {
            //see:
            //https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
            //Web API controllers don't have to check ModelState.IsValid if they have the [ApiController] attribute.
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.User.Add(user);
            _context.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("Update")]
    [Validation]
    public IActionResult update(User user)
    {
        try
        {
            var item = _context.User.FirstOrDefault(t => t.name == user.name);
            if (item == null)
                throw (new Exception());
            item.name = user.name;
            item.age = user.age;
            item.gender = user.gender;
            item.race = user.race;
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Delete/{name}")]
    public IActionResult delete(string name)
    {
        try
        {
            var users = new List<User>();
            var item = _users.FirstOrDefault(t => t.name == name);
            users.Remove(item);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getException")]
    [ExceptionRecover]
    public IActionResult GetException()
    {
        throw new Exception("This is a test");
    }

    [HttpGet("Binder/{user}")]
    public IActionResult BinderAdd(UserModel user)
    {
        try
        {
            return Ok(user.Id+"-"+user.Name);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("BinderAttribute/{id}")]
    public IActionResult GetById([ModelBinder(Name = "id")] UserModel user)
    {
        try
        {
            return Ok(user.Id + "-" + user.Name);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
