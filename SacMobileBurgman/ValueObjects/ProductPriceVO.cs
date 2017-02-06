using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class ProductPriceVO
    {
        public int ProductPriceID { get; set; }
        public int TablePriceID { get; set; }
        public int ProductID { get; set; }
        public decimal ProductMinPrice { get; set; }
        public decimal ProductMaxPrice { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Commission { get; set; }
    }
}