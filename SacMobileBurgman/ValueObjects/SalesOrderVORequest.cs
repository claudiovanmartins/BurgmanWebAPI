using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class SalesOrderVORequest
    {
        public int CustomerID { get; set; }
        public string OrderNumber { get; set; }
        public int TransactionsTypeID { get; set; }
        public DateTime ReleaseAt { get; set; }
        public string Freight { get; set; }
        public DateTime DeliveryAt { get; set; }
        public int TradeID { get; set; }
        public int TablePriceID { get; set; }
        public int CompanyID { get; set; }
        public string Observation { get; set; }
    }

}