using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class DealerCustomerController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult ListCustomers()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }
            
            long id1 = Convert.ToInt32(Session["ID"]);

            List<customer> customerlist = new List<customer>();
            customer objC = new customer();
            List<long?> customerids = model.ordermasters.Where(m => m.dealerid == id1).Select(m => m.uid).Distinct().ToList();
            //foreach (long item in customerids)
            //{
            //    long? customerid = item;
            //    objC = model.customers.Where(s => s.id == customerid).SingleOrDefault();
            //    if (objC != null)
            //    {
            //        customerlist.Add(objC);
            //    }
            //}
            
           List<CustomerWalletjoin> custwlt = (from ordermas in model.ordermasters.Where(m => m.dealerid == id1)
                                                join cust in model.customers on ordermas.uid equals cust.id
                                                join wall in model.wallets on cust.id equals wall.uid
                                               select new CustomerWalletjoin
                                               {
                                                   objcust = cust,
                                                   objwlt = wall
                                               }).Distinct().OrderByDescending(m => m.objcust.Addeddate).ToList();
            
            return View(custwlt);
        }

        public ActionResult CustomerDetail(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long id1 = Convert.ToInt32(id);
            
            CustomerAddressbookjoin join;

            join = (from cust in model.customers.Where(m => m.id == id1)
                    join addbook in model.addressbooks on cust.id equals addbook.Cid into list
                    select new CustomerAddressbookjoin
                    {
                        objcust = cust,
                        objaddbook = list,
                    }).SingleOrDefault();

            return View(join);
        }

    }
}