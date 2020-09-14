using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class CustomBook
    {
        public int bid { get; set; }
        public string bname { get; set; }
        public string bgener { get; set; }
        public long bprice { get; set; }
    }
}