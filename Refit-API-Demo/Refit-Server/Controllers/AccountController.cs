using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Refit_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private static List<AccountDto> dtos;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;            
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
    }
}
