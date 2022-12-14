using Entities;
using Refit;

namespace Refit_Client.Interfaces
{
    public interface IAccount
    {
        [Post("/api/Account/Login")]
        Task<HttpResponseMessage> Login(AccountDto dto);
    }
}
