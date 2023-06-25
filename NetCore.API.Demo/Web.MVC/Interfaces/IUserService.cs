using DataAccess;
using Refit;

namespace Web.MVC.Interfaces;

public interface IUserService
{
    [Get("/api/User/GetAll")]
    Task<List<User>> GetAll();
}
