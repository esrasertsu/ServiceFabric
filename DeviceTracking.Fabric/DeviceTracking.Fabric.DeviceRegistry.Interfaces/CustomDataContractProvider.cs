using Microsoft.ServiceFabric.Services.Remoting.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceTracking.Fabric.DeviceRegistry.Interfaces
{
    public class CustomDataContractProvider : IServiceRemotingMessageSerializationProvider
    {
        private readonly ServiceRemotingDataContractSerializationProvider serProvider;
        private readonly IEnumerable<Type> myTypes;//that need to be serialize for custom communication purpose

        public CustomDataContractProvider()
        {
            this.serProvider = new ServiceRemotingDataContractSerializationProvider();
            this.myTypes = new List<Type>()
            {
                typeof(Device),
                typeof(List<Device>)
            };

        }
        public IServiceRemotingMessageBodyFactory CreateMessageBodyFactory()
        {
            return this.serProvider.CreateMessageBodyFactory();
        }

        public IServiceRemotingRequestMessageBodySerializer CreateRequestMessageSerializer(Type serviceInterfaceType, IEnumerable<Type> requestWrappedTypes, IEnumerable<Type> requestBodyTypes = null)
        {
            var result = requestBodyTypes.Concat(this.myTypes);
            return this.serProvider.CreateRequestMessageSerializer(serviceInterfaceType, result);

        }

        public IServiceRemotingResponseMessageBodySerializer CreateResponseMessageSerializer(Type serviceInterfaceType, IEnumerable<Type> responseWrappedTypes, IEnumerable<Type> responseBodyTypes = null)
        {
            var result = responseBodyTypes.Concat(this.myTypes);
            return this.serProvider.CreateResponseMessageSerializer(serviceInterfaceType, result);

        }
    }
}
