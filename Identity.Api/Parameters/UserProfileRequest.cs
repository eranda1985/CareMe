using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Parameters
{
    public class UserProfileRequest
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "first")]
        public string First { get; set; }

        [JsonProperty(PropertyName = "last")]
        public string Last { get; set; }

        [JsonProperty(PropertyName = "mobile")]
        public string Mobile { get; set; }
    }
}
