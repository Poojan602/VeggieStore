using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class OrdermasterCustomerjoin
    {
        public ordermaster objordmaster { get; set; }
        public customer objcustomer { get; set; }

        public OrdermasterCustomerjoin()
        {
            objordmaster = new ordermaster();
            objcustomer = new customer();
        }
    }
}