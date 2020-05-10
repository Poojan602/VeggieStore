using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class AreaModel
    {
        public area area { get; set; }
        public areaselected areaselected { get; set; }

        public AreaModel()
        {
            area =new  area();
            areaselected = new areaselected();
        }
    }
}