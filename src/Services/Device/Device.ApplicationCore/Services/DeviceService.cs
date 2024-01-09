using Base.ApplicationCore.Interfaces;
using Device.ApplicationCore.Dtos;
using Device.ApplicationCore.Entities;
using Device.ApplicationCore.Interfaces.Repositories;
using Device.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Device.ApplicationCore.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public IEnumerable<Entities.Device> GetDevices()
    {
        return _deviceRepository.GetAll();
    }

    public DeviceDto GetDevice(int id)
    {
        var device =  _deviceRepository.GetById(id);
        var dto = new DeviceDto()
        {
            id = device.id,
            name = device.name,
            description= device.description,
            Number = device.deviceNumber,
            date= device.registedDate
        };
        return dto;
    }

    public IEnumerable<Entities.Device> SearchCondition(Expression<Func<Entities.Device, bool>> expression)
    {
        return _deviceRepository.GetByCondition(expression);
    }

    public bool Insert(Entities.Device device)
    {
        return _deviceRepository.Insert(device);
    }

    public bool Update(Entities.Device device)
    {
        return _deviceRepository.Update(device);
    }

    public bool Delete(int id)
    {
        return _deviceRepository.Delete(id);
    }

    public bool Delete(Entities.Device device)
    {
        return _deviceRepository.Delete(device);
    }
  
    public async Task<bool> DeleteAsync(Entities.Device device)
    {
        return await _deviceRepository.DeleteAsync(device);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _deviceRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Entities.Device>> GetDevicesAsync()
    {
        return await _deviceRepository.GetAllAsync();
    }

    public async Task<Entities.Device> GetDeviceAsync(int id)
    {
        return await _deviceRepository.GetByIdAsync(id);
    }

    public async Task<bool> InsertAsync(Entities.Device device)
    {
        return await _deviceRepository.InsertAsync(device);
    }
   
    public async Task<IEnumerable<Entities.Device>> SearchConditionAsync(Expression<Func<Entities.Device, bool>> expression)
    {
        return await _deviceRepository.GetByConditionAsync(expression);
    }
    
    public async Task<bool> UpdateAsync(Entities.Device device)
    {
        return await _deviceRepository.UpdateAsync(device);
    }

}
