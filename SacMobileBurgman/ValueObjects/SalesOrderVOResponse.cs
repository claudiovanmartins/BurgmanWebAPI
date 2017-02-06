using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class SalesOrderVOResponse
    {
        public int SalesOrderID { get; set; }
        public int CustomerID { get; set; }
        public string OrderNumber { get; set; }
        public int TransactionsTypeID { get; set; }
        public DateTime ReleaseAt { get; set; }
        public string Freight { get; set; }
        public DateTime DeliveryAt { get; set; }
        public int TradeID { get; set; }
        public string Observation { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int TotalItemsOrder { get; set; }
        public decimal AmountOrder { get; set; }
        

        public class SalesOrderResultModel
        {
            public int SalesOrderIDMobile { get; set; }
            public int SalesOrderIDRemote { get; set; }
        }
    }
}