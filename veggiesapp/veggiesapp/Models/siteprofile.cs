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
    
    public partial class siteprofile
    {
        public long id { get; set; }
        public string mshost { get; set; }
        public string msusername { get; set; }
        public string mspass { get; set; }
        public string contemail { get; set; }
        public string contccemail { get; set; }
        public string contbccemail { get; set; }
        public string addedip { get; set; }
        public Nullable<System.DateTime> addeddate { get; set; }
        public string modifyip { get; set; }
        public Nullable<System.DateTime> modifydate { get; set; }
    }
}
