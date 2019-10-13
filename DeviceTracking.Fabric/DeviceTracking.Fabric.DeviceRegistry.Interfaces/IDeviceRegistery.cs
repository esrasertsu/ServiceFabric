using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTracking.Fabric.DeviceRegistry.Interfaces
{
    public interface IDeviceRegistery : IService
    {
        Task<IEnumerable<Device>> GetDevices();

        Task<Device> GetDevice(Guid deviceId);

        Task AddDevice(Device device);
    }
}
 