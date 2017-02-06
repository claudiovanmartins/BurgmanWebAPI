using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class InventoryTypeVOResponse
    {
        public int InventoryTypeID { get; set; }
        public string Type { get; set; }
        public char Symbol { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}