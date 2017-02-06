using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class SacVORequest
    {
        public int UserID { get; set; }
        public int CustomerID { get; set; }
        public int SacTypeID { get; set; }
        public DateTime CreateAt { get; set; }
        public string Description { get; set; }
    }
}