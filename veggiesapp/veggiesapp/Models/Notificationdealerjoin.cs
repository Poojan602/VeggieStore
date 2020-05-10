using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class Notificationdealerjoin
    {
        public notification objnoti { get; set; }
        public dealer objdlr { get; set; }

        public Notificationdealerjoin()
        {
            objnoti = new notification();
            objdlr = new dealer();
        }
    }
}