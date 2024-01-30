using AutoMapper;
using Device.ApplicationCore.Dtos;

namespace Device.ApplicationCore.MappingProfiles;

public class DeviceProfiles : Profile
{
    public DeviceProfiles() 
    {
        CreateMap<Entities.Device, DeviceDto>();
        CreateMap<DeviceDto, Entities.Device>();
    }
}
