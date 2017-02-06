using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class TransactionTypeVOResponse
    {
        public int TransactionsTypeID { get; set; }
        public string TransactionsTypeDesc { get; set; }
        public char SyncMobile { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}