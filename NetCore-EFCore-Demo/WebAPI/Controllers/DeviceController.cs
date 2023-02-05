using Device.ApplicationCore.Dtos;
using Device.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using User.ApplicationCore.Dtos;
using User.ApplicationCore.Interfaces.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
    private readonly ILogger<DeviceController> _logger;
    private readonly IDeviceService _service;

    public DeviceController(ILogger<DeviceController> logger, IDeviceService service)
    {
        _logger = logger;
        _service = service;
    }

    //https://localhost:5001/device
    [HttpGet]
    public IEnumerable<object> GetDevices()
    {
        return _service.GetDevices();
    }

    //https://localhost:5001/device/async
    [HttpGet]
    [Route("async")]
    public async Task<IEnumerable<object>> GetDevicesAsync()
    {
        return await _service.GetDevicesAsync();
    }

    //https://localhost:5001/device/1
    [HttpGet]
    [Route("{id}")]
    public DeviceDto GetDevices(int id)
    {
        var dto = _service.GetDevice(id);
        return dto;
    }
}
