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
    using System.ComponentModel.DataAnnotations;

    public partial class productcategory1
    {
        public long id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        public string name { get; set; }

        public Nullable<bool> visible { get; set; }
        public Nullable<long> sortorder { get; set; }
        public Nullable<System.DateTime> addeddate { get; set; }
        public string addedip { get; set; }
        public Nullable<System.DateTime> modifydate { get; set; }
        public string modifyip { get; set; }
        public string image { get; set; }
        public Nullable<long> dealerid { get; set; }
    }
}
