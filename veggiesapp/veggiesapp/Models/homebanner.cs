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
    using System.Configuration;

    public partial class homebanner
    {
        public long id { get; set; }
        public Nullable<bool> visible { get; set; }
        public string image { get; set; }
        public string link { get; set; }
        public Nullable<long> sortorder { get; set; }
        public Nullable<System.DateTime> addeddate { get; set; }
        public string addedip { get; set; }
        public Nullable<System.DateTime> modifydate { get; set; }
        public string modifyip { get; set; }
        public Nullable<long> dealerid { get; set; }
        public Nullable<bool> ismainpage { get; set; }
        public string imagepath
        {
            get
            {
                return ConfigurationManager.AppSettings["imagepath"].ToString() + image;
            }
        }
    }
}
