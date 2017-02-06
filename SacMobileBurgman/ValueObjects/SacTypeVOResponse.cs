using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class SacTypeVOResponse
    {
        public int SacTypeID { get; set; }
        public string Description { get; set; }
        public char Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}