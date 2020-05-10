using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class DealerCityJoin
    {
        public city objcity { get; set; }
        public dealer objdlr { get; set; }

        public DealerCityJoin()
        {
            objcity = new city();
            objdlr = new dealer();
        }
    }
}