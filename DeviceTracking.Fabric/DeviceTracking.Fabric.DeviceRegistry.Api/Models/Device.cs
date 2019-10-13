using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceTracking.Fabric.DeviceRegistry.Api.Models
{
    public class Device
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("registerDate")]
        public DateTime RegisterDate { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
