using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class CustomUser
    {
        public int uid { get; set; }
        public string uname { get; set; }
        public long ucontact { get; set; }
        public string uaddress { get; set; }
    }
}