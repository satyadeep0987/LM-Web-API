//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bookissue
    {
        public int iis { get; set; }
        public Nullable<int> uid { get; set; }
        public Nullable<int> bid { get; set; }
        public string issuedate { get; set; }
        public int noofdays { get; set; }
        public string expreturndate { get; set; }
        public string actualreturndate { get; set; }
        public Nullable<long> fine { get; set; }
    
        public virtual book book { get; set; }
        public virtual user user { get; set; }
    }
}