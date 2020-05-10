using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using veggiesapp.Models;

namespace veggiesapp.Models
{
    public class voucherareajoin
    {
        public voucher objvc { get; set; }
        public area objar { get; set; }

        public voucherareajoin()
        {
            objvc = new voucher();
            objar = new area();
        }
    }
}