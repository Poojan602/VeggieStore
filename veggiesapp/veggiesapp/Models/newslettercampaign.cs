//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace veggiesapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class newslettercampaign
    {
        public long id { get; set; }
        public string fromEmail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<System.DateTime> addeddate { get; set; }
        public string addedip { get; set; }
        public Nullable<System.DateTime> modifydate { get; set; }
        public string modifyip { get; set; }
        public Nullable<long> subscriberCnt { get; set; }
    }
}
