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

    public partial class notification
    {
        public long id { get; set; }

        [Required(ErrorMessage ="Message is compulsory")]
        public string Message { get; set; }

        public Nullable<System.DateTime> addeddate { get; set; }
        public string image { get; set; }
        public Nullable<long> dealerid { get; set; }
        public List<checkboxnotification> check { get; set; }

    }

    public partial class checkboxnotification
    {
        public long aid { get; set; }
        public string aname { get; set; }
        public bool ticked { get; set; }
    }
}
