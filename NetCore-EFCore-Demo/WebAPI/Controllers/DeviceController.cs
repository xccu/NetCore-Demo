using Device.ApplicationCore.Dtos;
using Entities = Device.ApplicationCore.Entities;
using Device.ApplicationCore.Interfaces.Services;
using Device.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Utilities.Zlib;
using System;
using User.ApplicationCore.Dtos;
using User.ApplicationCore.Interfaces.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
    private readonly ILogger<DeviceController> _logger;
    private readonly IDeviceService _service;
    private DeviceContext _context;

    public DeviceController(ILogger<DeviceController> logger, IDeviceService service, DeviceContext context)
    {
        _logger = logger;
        _service = service;
        _context = context;
    }

    [HttpGet]
    [Route("query")]
    public string GetQuery()
    {
        DateTime date= DateTime.Now;
        IQueryable<Entities.Device> query = this._context.Device;
        query = query.Where(t => t.registedDate <= date);
        string str = query.ToQueryString();
        

        _logger.LogDebug(query.ToQueryString());

        var result = query.ToList();
        return str;
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
