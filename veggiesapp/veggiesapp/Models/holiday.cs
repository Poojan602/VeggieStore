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

    public partial class holiday
    {
        public long id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string title { get; set; }

        public Nullable<System.DateTime> addeddate { get; set; }
        public string addedip { get; set; }
        public Nullable<System.DateTime> modifydate { get; set; }
        public string ModifyIp { get; set; }

        [Required(ErrorMessage = "Please Select Holiday Date")]
        public Nullable<System.DateTime> holidaydate { get; set; }

        public Nullable<long> dealerid { get; set; }

        public List<checkboxholiday> check { get; set; }

    }

    public partial class checkboxholiday
    {
        public long arid { get; set; }
        public string arname { get; set; }
        public bool ischeck { get; set; }
    }
}