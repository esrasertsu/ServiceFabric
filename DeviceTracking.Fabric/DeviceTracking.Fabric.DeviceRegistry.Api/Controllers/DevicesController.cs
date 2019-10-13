using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceTracking.Fabric.DeviceRegistry.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace DeviceTracking.Fabric.DeviceRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {

        private readonly IDeviceRegistery _deviceRegistryService;

        public DevicesController()
        {
            var serviceProxyFactory = new ServiceProxyFactory((C) => new FabricTransportServiceRemotingClientFactory(
                serializationProvider: new CustomDataContractProvider()));

            _deviceRegistryService = serviceProxyFactory.CreateServiceProxy<IDeviceRegistery>(
               new Uri("fabric:/DeviceTracking.Fabric/DeviceTracking.Fabric.DeviceRegistry"),
               new ServicePartitionKey(0), TargetReplicaSelector.PrimaryReplica);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Models.Device>> Get()
        {
            IEnumerable<Interfaces.Device> devices = await _deviceRegistryService.GetDevices();

            return devices.OrderBy(d => d.RegisterDate)
                .Select(d => new Models.Device
                {
                    Id = d.Id,
                    RegisterDate = d.RegisterDate,
                    Status = d.Status,
                    Type = d.Type
                });
        }

        public async Task Post([FromBody] Models.Device device)
        {
            var newDevice = new DeviceRegistry.Interfaces.Device()
            {
                Id = device.Id,
                RegisterDate = device.RegisterDate,
                Status = device.Status,
                Type = device.Type
            };

            await _deviceRegistryService.AddDevice(newDevice);
        }
      
    }
}
