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

    public partial class voucher
    {
        public long id { get; set; }

        [Required(ErrorMessage ="Code should not be blank")]
        public string Code { get; set; }
        public Nullable<short> Type { get; set; }

        [Required(ErrorMessage = "Voucher Amount should not be blank")]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "Voucher Amount Must be Numeric")]
        public Nullable<decimal> VoucherAmount { get; set; }

        [Required(ErrorMessage = "Minimum Amount should not be blank")]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "Minimum Amount Must be Numeric")]
        public Nullable<decimal> MinimumAmount { get; set; }

        [Required(ErrorMessage = "PublishDate cannot be blank")]
        public Nullable<System.DateTime> PublishDate { get; set; }

        [Required(ErrorMessage = "ExpiryDate cannot be blank")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        public Nullable<short> Towhom { get; set; }
        public string AdminNote { get; set; }
        public Nullable<System.DateTime> addeddate { get; set; }
        public string addedIp { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyIp { get; set; }
        public Nullable<bool> Sent { get; set; }
        public Nullable<bool> SenttoAll { get; set; }
        public Nullable<long> areaid { get; set; }

        public List<checkbox> check { get; set; }

    }

    public partial class checkbox
    {
        public long areaid { get; set; }
        public string areaname { get; set; }
        public bool ischecked { get; set; }
    }
}
