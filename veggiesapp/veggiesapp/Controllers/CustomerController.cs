using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class CustomerController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult suspendedcustomer()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            IEnumerable<CustomerWalletjoin> custwlt;

            custwlt = (from wal in model.wallets
                       join cust in model.customers.Where(m => m.Status == false) on wal.uid equals cust.id
                       select new CustomerWalletjoin
                       {
                           objcust = cust,
                           objwlt = wal
                       }).OrderByDescending(m => m.objcust.Addeddate).ToList();
            
            return View(custwlt);
        }

        public ActionResult activecustomer()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            IEnumerable<CustomerWalletjoin> custwlt;

            custwlt = (from wal in model.wallets
                       join cust in model.customers.Where(m => m.Status == true) on wal.uid equals cust.id
                       select new CustomerWalletjoin
                       {
                           objcust = cust,
                           objwlt = wal
                       }).OrderByDescending(m => m.objcust.Addeddate).ToList();

            return View(custwlt);
        }

        public ActionResult activetosuspend(string id)
        {
            long id1 = (long)Convert.ToDouble(id);

            var cust = new customer()
            {
                id = id1,
                Status = false
            };

            using (sabziappEntities model = new sabziappEntities())
            {
                model.customers.Attach(cust);
                model.Entry(cust).Property(x => x.Status).IsModified = true;
                model.SaveChanges();
            }

            return RedirectToAction("activecustomer");

        }

        public ActionResult suspendtoactive(string id)
        {
            long id1 = (long)Convert.ToDouble(id);

            var cust = new customer()
            {
                id = id1,
                Status = true
            };

            using (sabziappEntities model = new sabziappEntities())
            {
                model.customers.Attach(cust);
                model.Entry(cust).Property(x => x.Status).IsModified = true;
                model.SaveChanges();
            }

            return RedirectToAction("suspendedcustomer");
        }

        public ActionResult viewcustdetails(string id)
        {
            using (sabziappEntities model = new sabziappEntities())
            {
                long id1 = (long)Convert.ToDouble(id);

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
}