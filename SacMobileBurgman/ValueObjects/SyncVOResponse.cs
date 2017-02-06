using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class SyncVOResponse
    {
        public int ModuleID { get; set; }
        public string Module { get; set; }
        public int CountRows { get; set; }
    }
}