using Refit;
using User.ApplicationCore.Dtos;

namespace RazorPage.Web.HttpAggregator.Services.Interfaces;

public interface IHttpUserService
{
    [Get("/user")]
    Task<IEnumerable<UserDto>> GetAllAsync();
    [Get("/user/{id}")]
    Task<UserDto> GetAsync(string id);
    [Post("/user")]
    Task<bool> CreateAsync(UserDto dto);
    [Put("/user")]
    Task<bool> UpdateAsync(UserDto dto);
    [Delete("/user/{id}")]
    Task<bool> DeleteAsync(string id);
}
