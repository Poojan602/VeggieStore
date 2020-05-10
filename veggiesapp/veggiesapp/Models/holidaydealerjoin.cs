using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class holidaydealerjoin
    {
        public holiday objhld { get; set; }
        public dealer objdlr { get; set; }

        public holidaydealerjoin()
        {
            objhld = new holiday();
            objdlr = new dealer();
        }
    }
}