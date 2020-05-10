using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace veggiesapp.Models
{
    public class DealerAreamodelSubscriptionselectedDeliveryslotJoin
    {
        public dealer objdlr { get; set; }
        public subscriptionselected objsubselected { get; set; }
        public List<deliveryslot> objdeliveryslot { get; set; }
        public city objct { get; set; }
        public List<area> area { get; set; }
        public List<AreaModel> areamodel { get; set; }

        public DealerAreamodelSubscriptionselectedDeliveryslotJoin()
        {
            objdlr = new dealer();
            objsubselected = new subscriptionselected();
            objdeliveryslot = new List<deliveryslot>();
            objct = new city();
            areamodel = new List<AreaModel>();
            area = new List<area>();
        }
    }
}