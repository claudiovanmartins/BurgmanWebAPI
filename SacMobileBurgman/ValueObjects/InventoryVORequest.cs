using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class InventoryVORequest
    {
        [JsonProperty(PropertyName = "BarCode")]
        public string BarCode { get; set; }

        [JsonProperty(PropertyName = "Latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "StatusID")]
        public int StatusID { get; set; }

        [JsonProperty(PropertyName = "InventoryTypeID")]
        public int InventoryTypeID { get; set; }

        public int UserID { get; set; }

    }
}