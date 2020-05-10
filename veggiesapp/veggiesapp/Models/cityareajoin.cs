using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class cityareajoin
    {
        public city objcity { get; set; }
        public area objarea { get; set; }

        public cityareajoin()
        {
            objcity = new city();
            objarea = new area();
        }
    }
}