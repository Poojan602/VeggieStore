using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class CustomerWalletjoin
    {
        public customer objcust { get; set; }
        public wallet objwlt { get; set; }
        public ordermaster ordermaster { get; set; }

        public CustomerWalletjoin()
        {
            objcust = new customer();
            ordermaster = new ordermaster();
            objwlt = new wallet();
        }
    }
}