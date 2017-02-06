using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class UserVORequest
    {
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
    }
}