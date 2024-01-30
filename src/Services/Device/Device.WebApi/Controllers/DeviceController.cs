using AutoMapper;
using Device.ApplicationCore.Dtos;
using Device.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Entities = Device.ApplicationCore.Entities;

namespace Device.WebApi.Controllers;

[ApiController]
[Route("api/device")]
public class DeviceController : ControllerBase
{
    private readonly ILogger<DeviceController> _logger;
    private readonly IDeviceService _service;
    private readonly IMapper _mapper;

    public DeviceController(ILogger<DeviceController> logger, IDeviceService service, IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    //https://localhost:5001/device
    [HttpGet]
    public async Task<IEnumerable<DeviceDto>> GetAllAsync()
    {
        var devices =  await _service.GetDevicesAsync();
        var dto = _mapper.Map<Entities.Device[], IEnumerable<DeviceDto>>(devices.ToArray());
        return dto;
    }

    //https://localhost:5001/device/1
    [HttpGet]
    [Route("{id}")]
    public async Task<DeviceDto> GetAsync(int id)
    {
        var device = await _service.GetDeviceAsync(id);
        var dto = _mapper.Map<DeviceDto>(device);
        return dto;
    }

    [HttpPost]
    [DeviceActionFilter]
    public async Task<bool> CreateAsync(DeviceDto dto)
    {

        try 
        {
            var device = _mapper.Map<Entities.Device>(dto);
            return await _service.InsertAsync(device);
        }
        catch (Exception ex) 
        {
            return await Task.FromResult(false);
        }
       
    }

    [HttpPut]
    public async Task<bool> UpdateAsync(DeviceDto dto)
    {
        var device = _mapper.Map<Entities.Device>(dto);
        return await _service.UpdateAsync(device);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(int id)
    {
        var device = await _service.GetDeviceAsync(id);
        return await _service.DeleteAsync(device);
    }

}

