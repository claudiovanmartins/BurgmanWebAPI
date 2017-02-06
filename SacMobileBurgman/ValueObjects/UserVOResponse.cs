using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class UserVOResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int CompanyID { get; set; }
        public int TradeID { get; set; }
        public string TypeUserSymbol { get; set; }
        public string Token { get; set; }
    }
}