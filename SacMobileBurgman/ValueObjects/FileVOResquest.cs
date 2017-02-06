using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class FileVOResquest
    {
        public int UserID { get; set; }
        public int CustomerID { get; set; }
        public string NameImage { get; set; }
        public string AttachmentFile { get; set; }
        public string Comment { get; set; }
    }
}