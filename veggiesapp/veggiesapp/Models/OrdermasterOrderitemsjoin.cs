using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class OrdermasterOrderitemsjoin
    {
        public ordermaster objordermaster { get; set; }
        public IEnumerable<orderitem> objorderitem { get; set; }

        public OrdermasterOrderitemsjoin()
        {
            objordermaster = new ordermaster();
            objorderitem = new List<orderitem>();
        }
    }
}