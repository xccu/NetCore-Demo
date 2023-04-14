using Models;
using Refit;
using System;

namespace Refit_Client_Demo.Interfaces;

public interface IUserApi
{
    [Get("/api/User/GetAll")]
    Task<List<User>> GetAll();

    [Post("/api/User/Add")]
    Task Add(User user);

    [Put("/api/User/Update/{name}")]
    Task Update(string name, User user);

    [Delete("/api/User/Delete/{name}")]
    Task Delete(string name);

}
