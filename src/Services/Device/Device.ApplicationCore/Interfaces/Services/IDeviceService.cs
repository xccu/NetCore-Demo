using Device.ApplicationCore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Device.ApplicationCore.Interfaces.Services;

public interface IDeviceService
{
    public IEnumerable<Entities.Device> GetDevices();
    public DeviceDto GetDevice(int id);
    public IEnumerable<Entities.Device> SearchCondition(Expression<Func<Entities.Device, bool>> expression);
    public bool Update(Entities.Device device);
    public bool Insert(Entities.Device device);
    public bool Delete(Entities.Device device);
    public bool Delete(int id);
    public Task<IEnumerable<Entities.Device>> GetDevicesAsync();
    public Task<Entities.Device> GetDeviceAsync(int id);
    public Task<IEnumerable<Entities.Device>> SearchConditionAsync(Expression<Func<Entities.Device, bool>> expression);
    public Task<bool> UpdateAsync(Entities.Device device);
    public Task<bool> InsertAsync(Entities.Device device);
    public Task<bool> DeleteAsync(Entities.Device device);
    public Task<bool> DeleteAsync(int id);
}
