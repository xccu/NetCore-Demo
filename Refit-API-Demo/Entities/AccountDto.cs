using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class AccountDto
    {
        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}