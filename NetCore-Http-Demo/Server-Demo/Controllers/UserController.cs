using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Server_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private static List<User> _users;
        private UserDbContext _context;

        public UserController(ILogger<UserController> logger,UserDbContext context)
        {
            _logger = logger;
            _context = context;
            InitialUsers();
        }

        [HttpGet("GetAll")]
        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList();
            //return _users;
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

        [HttpPut("Update/{name}")]
        public IActionResult update(string name,User user)
        {
            try
            {
                var item = _users.FirstOrDefault(t => t.name == name);
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

        private void InitialUsers() 
        {
            if (_users != null)
                return;
            _users = new List<User>();
            _users.Add(new User() { name = "Weslie", age = 12, gender = "male", race = "Caprinae" });
            _users.Add(new User() { name = "Wolffy", age = 34, gender = "male", race = "Lupo" });
            _users.Add(new User() { name = "Tibby", age = 11, gender = "female", race = "Caprinae" });
            _users.Add(new User() { name = "Sparky", age = 13, gender = "male", race = "Caprinae" });
            _users.Add(new User() { name = "Paddi", age = 10, gender = "male", race = "Caprinae" });
            _users.Add(new User() { name = "Jonie", age = 13, gender = "female", race = "Caprinae" });
            _users.Add(new User() { name = "Slowy", age = 80, gender = "male", race = "Caprinae" });
            _users.Add(new User() { name = "Wolnie", age = 33, gender = "female", race = "Lupo" });
            _users.Add(new User() { name = "Wilie", age = 5, gender = "male", race = "Lupo" });
        }
    }
}
