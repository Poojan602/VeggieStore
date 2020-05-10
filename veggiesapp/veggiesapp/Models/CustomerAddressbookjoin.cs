using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class CustomerAddressbookjoin
    {
        public customer objcust { get; set; }
        public IEnumerable<addressbook> objaddbook { get; set; }

        public CustomerAddressbookjoin()
        {
            objcust = new customer();
            objaddbook = new List<addressbook>();
        }
    }
}