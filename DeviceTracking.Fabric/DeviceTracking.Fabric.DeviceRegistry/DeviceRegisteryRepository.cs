using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceTracking.Fabric.DeviceRegistry.Interfaces;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;

namespace DeviceTracking.Fabric.DeviceRegistry
{
    public class DeviceRegisteryRepository : IDeviceRegistryRepository
    {
        private readonly IReliableStateManager stateManager;

        public DeviceRegisteryRepository(IReliableStateManager stateManager)
        {
            this.stateManager = stateManager;
        }
        public async Task AddDevice(Device device)
        {
            var devices = await stateManager.GetOrAddAsync<IReliableDictionary<Guid, Device>>("devices");

            using (var trx = stateManager.CreateTransaction())
            {
                await devices.AddOrUpdateAsync(trx, device.Id, device, (id, value) => device);
                await trx.CommitAsync();
            }
        }

        public async Task<Device> GetDevice(Guid deviceId)
        {
            var devices = await stateManager.GetOrAddAsync<IReliableDictionary<Guid, Device>>("devices");

            using (var trx = stateManager.CreateTransaction())
            {
               ConditionalValue<Device> conditionalValue = await devices.TryGetValueAsync(trx,deviceId);

                if (conditionalValue.HasValue)
                    return conditionalValue.Value;
            }

            return null;
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            var devices = await stateManager.GetOrAddAsync<IReliableDictionary<Guid, Device>>("devices");
            List<Device> result = new List<Device>();

            using (var trx = stateManager.CreateTransaction())
            {
                var allDevices = await devices.CreateEnumerableAsync(trx, EnumerationMode.Unordered);

                using (var enumarator = allDevices.GetAsyncEnumerator())
                {
                    while (await enumarator.MoveNextAsync(CancellationToken.None))
                    {
                        KeyValuePair<Guid, Device> keyValuePair = enumarator.Current;
                        result.Add(keyValuePair.Value);
                    }
                }

            }

            return result;
        }
    }
}
