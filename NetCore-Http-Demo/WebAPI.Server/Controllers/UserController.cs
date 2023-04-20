using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server_Demo.Attributes;
using WebAPI.Server.Attributes;

namespace WebAPI.Server.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    //[Validation]
    [ExceptionRecover]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private static List<User> _users;
        private UserDbContext _context;

        public UserController(ILogger<UserController> logger,UserDbContext context)
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
            return _context.User.FirstOrDefault(t=>t.name == name);
        }

        [HttpPost("Add")]
        public IActionResult Add(User user)
        {
            try
            {
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
        public IActionResult update(User user)
        {
            try
            {
                var item = _context.User.FirstOrDefault(t => t.name == user.name);
                if (item == null)
                    throw(new Exception());
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
        public IActionResult GetException()
        {
            throw new Exception("This is a test");
        }

    }
}
