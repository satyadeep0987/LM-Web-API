using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class CustomIssue
    {
        public int iis { get; set; }
        public string username { get; set; }
        public string bookname { get; set; }
        public string issuedate { get; set; }
        public int noofdays { get; set; }
        public string expreturndate { get; set; }
        public string actualreturndate { get; set; }
        public long? fine { get; set; }
    }
}