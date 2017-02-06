using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class ProductVO
    {
        public int ProductID { get; set; }
        public string Product { get; set; }
        public decimal ProductInventoryQuantity { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string MeasureUnit { get; set; }
    }
}