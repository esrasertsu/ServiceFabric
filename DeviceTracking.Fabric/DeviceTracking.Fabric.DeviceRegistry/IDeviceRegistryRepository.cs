using DeviceTracking.Fabric.DeviceRegistry.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTracking.Fabric.DeviceRegistry
{
    public interface IDeviceRegistryRepository
    {
        Task<IEnumerable<Device>> GetDevices();

        Task<Device> GetDevice(Guid deviceId);

        Task AddDevice(Device device);
    }
}
