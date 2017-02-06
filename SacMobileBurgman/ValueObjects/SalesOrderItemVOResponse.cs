using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class SalesOrderItemVOResponse
    {
        public int SalesOrderItemID { get; set; }
        public int SalesOrderID { get; set; }
        public int ProductID { get; set; }
        public decimal Commission { get; set; }
        public decimal ProductQuantity { get; set; }
        public decimal ProductUnitaryValue { get; set; }
        public decimal AmountOrderItem { get; set; }
        public char FlagMobile { get; set; }
    }
    
}