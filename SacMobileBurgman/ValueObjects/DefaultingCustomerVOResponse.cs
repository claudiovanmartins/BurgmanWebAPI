using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class DefaultingCustomerVOResponse
    {
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int DuplicatesQuantity { get; set; }
        public decimal DuplicatesValue { get; set; }
    }
}