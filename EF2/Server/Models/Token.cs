using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Token
    {
        [JsonProperty(PropertyName = "staffId")]
        public string StaffId { get; set; }

        [JsonProperty(PropertyName = "signToken")]
        public Guid SignToken { get; set; }

        [JsonProperty(PropertyName = "expireTime")]
        public DateTime ExpireTime { get; set; }
    }
}