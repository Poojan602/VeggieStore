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
    
    public partial class sampleproduct
    {
        public long id { get; set; }
        public string Title { get; set; }
        public string HindiTitle { get; set; }
        public Nullable<long> cat1id { get; set; }
        public Nullable<long> cat2id { get; set; }
        public string DESCabout { get; set; }
        public string DESCbenifits { get; set; }
        public string DESChowtouse { get; set; }
        public string UOM { get; set; }
        public Nullable<bool> visible { get; set; }
        public Nullable<bool> featured { get; set; }
        public Nullable<long> sortorder { get; set; }
        public Nullable<System.DateTime> addeddate { get; set; }
        public string addedip { get; set; }
        public Nullable<System.DateTime> modifydate { get; set; }
        public string modifyip { get; set; }
        public Nullable<long> cityid { get; set; }
        public Nullable<long> dealerid { get; set; }
        public Nullable<byte> type { get; set; }
    }
}