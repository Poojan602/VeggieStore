using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class dealersubscriptionselected
    {
        public dealer dealer { get; set; }

        public List<subscription> subscriptions { get; set; }

        [Required(ErrorMessage ="Please Select Any one Subscription from the options")]
        public string selectedsubscriptionid { get; set; }
    }
}