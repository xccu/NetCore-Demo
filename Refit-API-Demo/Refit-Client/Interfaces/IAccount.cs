using Entities;
using Refit;

namespace Refit_Client.Interfaces
{
    public interface IAccount
    {

        [Post("/api/Account/Login")]
        Task<HttpResponseMessage> LoginAsync(AccountDto dto);

        [Get("/api/Account/All")]
        Task<IEnumerable<AccountDto>?> GetAllAsync();
    }
}
