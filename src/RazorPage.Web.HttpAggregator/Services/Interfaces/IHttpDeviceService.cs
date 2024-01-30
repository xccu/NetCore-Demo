using Device.ApplicationCore.Dtos;
using Refit;
using User.ApplicationCore.Dtos;

namespace RazorPage.Web.HttpAggregator.Services.Interfaces;

public interface IHttpDeviceService
{
    [Get("/api/device")]
    Task<IEnumerable<DeviceDto>> GetAllAsync();
    [Get("/api/device/{id}")]
    Task<DeviceDto> GetAsync(int id);
    [Post("/api/device")]
    Task<bool> CreateAsync(DeviceDto dto);
    [Put("/api/device")]
    Task<bool> UpdateAsync(DeviceDto dto);
    [Delete("/api/device/{id}")]
    Task<bool> DeleteAsync(int id);
}
