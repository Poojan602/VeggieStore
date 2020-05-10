using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class CustomerAreajoin
    {
        public customer Objcutomer { get; set; }
        public area Objarea { get; set; }

        public CustomerAreajoin()
        {
            Objcutomer = new customer();
            Objarea = new area();
        }
    }
}