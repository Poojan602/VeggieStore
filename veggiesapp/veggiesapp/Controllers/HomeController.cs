using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class HomeController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult Index()
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
                    }).ToList().OrderByDescending(m => m.objordmaster.Orderdate).Take(5);

            long totalcust = model.customers.Count();
            ViewBag.totalcust = totalcust;
            ViewBag.totaldlr = model.dealers.Count();

            List<ordermaster> om = model.ordermasters.ToList();

            long total = 0;
            foreach (var item in om)
            {
                total = total + Convert.ToInt32(item.Grandtotal);
            }
            ViewBag.totalrevenue = total;

            if (totalcust == 0)
            {
                ViewBag.avgrevenue = 0;
            }
            else
            {
                ViewBag.avgrevenue = total / totalcust;
            }
            
            ViewBag.returneditems = model.ordermasters.Where(m => m.OrderStatus == 5).Count();

            List<decimal?> ordm = model.ordermasters.Where(m => m.OrderStatus == 6).Select(m => m.Grandtotal).ToList();

            decimal? total12 = 0;
            foreach (var item in ordm)
            {
                total12 = total12 + item;
            }
            ViewBag.totalreturnvalue = total12;

            List<decimal?> vegetablepricelist = model.orderitems.Where(m => m.cat1id == 10).Select(m => m.amount).ToList();
            List<decimal?> fruitpricelist = model.orderitems.Where(m => m.cat1id == 11).Select(m => m.amount).ToList();

            return View(list);
        }

    }
}