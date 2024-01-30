using AutoMapper;
using Device.ApplicationCore.Dtos;
using Device.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using RazorPage.Web.HttpAggregator.Services.Interfaces;

namespace RazorPage.Web.HttpAggregator.Controllers;

[ApiController]
[Route("api/device")]
public class DeviceController : Controller
{
    private readonly ILogger<DeviceController> _logger;
    private readonly IHttpDeviceService _service;


    public DeviceController(ILogger<DeviceController> logger, IHttpDeviceService service)
    {
        _logger = logger;
        _service = service;
    }

    //https://localhost:5001/device
    [HttpGet]
    public async Task<IEnumerable<DeviceDto>> GetAllAsync()
    {
        return await _service.GetAllAsync();      
    }

    //https://localhost:5001/device/1
    [HttpGet]
    [Route("{id}")]
    public async Task<DeviceDto> GetAsync(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    public async Task<bool> CreateAsync(DeviceDto dto)
    {
        return await _service.CreateAsync(dto);
    }

    [HttpPut]
    public async Task<bool> UpdateAsync(DeviceDto dto)
    {
        return await _service.UpdateAsync(dto);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(int id)
    {
       return await _service.DeleteAsync(id);
    }

}
