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
    
    public partial class deliveryslot
    {
        public int id { get; set; }
        public string title { get; set; }
        public string timeslot { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<System.DateTime> modifydate { get; set; }
        public string modifyip { get; set; }
        public Nullable<int> modifyby { get; set; }
        public Nullable<long> totalorders { get; set; }
        public Nullable<bool> isactive { get; set; }
        public Nullable<long> Dealerid { get; set; }
    }
}