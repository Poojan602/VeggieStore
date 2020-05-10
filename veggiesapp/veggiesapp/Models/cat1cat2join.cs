using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class cat1cat2join
    {
        public productcategory1 objcat1 { get; set; }
        public productcategory2 objcat2 { get; set; }

        public cat1cat2join()
        {
            objcat1 = new productcategory1();
            objcat2 = new productcategory2();
        }
    }
}