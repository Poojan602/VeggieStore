using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using veggiesapp.Models;

namespace veggiesapp.Models
{
    public class productprice
    {
        public product product { get; set; }
        public pricemanager pricemanager { get; set; }
    }
}