using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class ReviewController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult viewreviews()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult reviewdetail(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = Convert.ToInt32(id);
            OrdermasterOrderitemsjoin single;

            single = (from master in model.ordermasters.Where(m => m.id == id1)
                      join items in model.orderitems on master.id equals items.Ordid into list
                      select new OrdermasterOrderitemsjoin
                      {
                          objordermaster = master,
                          objorderitem = list,
                      }).SingleOrDefault();

            return View(single);
        }

        public ActionResult deleteorder(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = Convert.ToInt32(id);

            ordermaster om = model.ordermasters.Where(m => m.id == id1).SingleOrDefault();

            List<orderitem> oi = model.orderitems.Where(m => m.Ordid == id1).ToList();

            model.ordermasters.Remove(om);
            model.SaveChanges();

            foreach (var item in oi)
            {
                model.orderitems.Remove(item);
                model.SaveChanges();
            }
            
            return RedirectToAction("viewreviews");
        }

        public ActionResult printorder()
        {
            return View();
        }
    }
}