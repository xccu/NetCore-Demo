using Refit;
using User.ApplicationCore.Dtos;

namespace RazorPage.Web.HttpAggregator.Services.Interfaces;

public interface IHttpUserService
{
    [Get("/api/user")]
    Task<IEnumerable<UserDto>> GetAllAsync();
    [Get("/api/user/{id}")]
    Task<UserDto> GetAsync(string id);
    [Post("/api/user")]
    Task<bool> CreateAsync(UserDto dto);
    [Put("/api/user")]
    Task<bool> UpdateAsync(UserDto dto);
    [Delete("/api/user/{id}")]
    Task<bool> DeleteAsync(string id);

   
}
