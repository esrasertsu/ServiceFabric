using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeviceTracking.Fabric.DeviceRegistry.Interfaces
{
    [DataContract]
    public class Device
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public DateTime RegisterDate { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
