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
    
    public partial class cart
    {
        public long id { get; set; }
        public string custid { get; set; }
        public Nullable<long> Proid { get; set; }
        public Nullable<long> priceid { get; set; }
        public Nullable<long> areaid { get; set; }
        public Nullable<long> Quantity { get; set; }
        public Nullable<byte> isspecial { get; set; }
        public string instruction { get; set; }
    }
}