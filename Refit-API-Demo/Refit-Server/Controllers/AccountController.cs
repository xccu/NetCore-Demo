using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Refit_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private static List<AccountDto> _dtos;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;   
            InitialData();
        }

        [HttpPost("Login")]
        public IActionResult Login(AccountDto dto)
        {
            try
            {
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All")]
        public IEnumerable<AccountDto> GetAll()
        {
            return _dtos;          
        }

        private void InitialData()
        {
            if (_dtos != null)
                return;
            _dtos = new List<AccountDto>();
            _dtos.Add(new AccountDto() { UserName = "Admin", Password = "123" });
            _dtos.Add(new AccountDto() { UserName = "User1", Password = "456" });
            _dtos.Add(new AccountDto() { UserName = "User2", Password = "456" });
        }
    }
}
