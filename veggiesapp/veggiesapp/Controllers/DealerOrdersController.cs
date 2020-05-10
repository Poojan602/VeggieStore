using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class DealerOrdersController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult Unconfirm()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.OrderStatus == 0 && m.dealerid == dlrid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult confirm()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.OrderStatus == 1 && m.dealerid == dlrid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult packing()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.OrderStatus == 2 && m.dealerid == dlrid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult outfordelivery()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.OrderStatus == 3 && m.dealerid == dlrid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult delivered()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.OrderStatus == 4 && m.dealerid == dlrid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult returnorder()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.OrderStatus == 5 && m.dealerid == dlrid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult refunded()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.OrderStatus == 6 && m.dealerid == dlrid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult allorders()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.dealerid == dlrid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).OrderByDescending(m => m.objordmaster.Orderdate).ToList();

            return View(list);
        }

        public ActionResult orderdetail(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);
            long id1 = Convert.ToInt32(id);
            OrdermasterOrderitemsjoin single;

            single = (from master in model.ordermasters.Where(m => m.id == id1 && m.dealerid == dlrid)
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
                Response.Redirect("~/DealerLogin/loginpage");
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

            string controllername = TempData["Status"].ToString();

            return RedirectToAction(controllername,"DealerOrders");
        }

        public ActionResult sales()
        {
            return View();
        }

        public ActionResult demandsheet()
        {
            return View();
        }

        public ActionResult statuschange(long status, string comment, long ordermasterid)
        {
            ordermaster om = model.ordermasters.Where(m => m.id == ordermasterid).SingleOrDefault();

            om.Admincomments = comment;
            om.OrderStatus = status;

            model.Entry(om).State = EntityState.Modified;
            model.SaveChanges();

            return RedirectToAction("orderdetail", "DealerOrders", new { id = ordermasterid });
        }

        //public ActionResult refundbtn(long id)
        //{
        //    long ordermasterid = id;

        //    ordermaster om = model.ordermasters.Where(m => m.id == ordermasterid).SingleOrDefault();

        //    om.OrderStatus = 6;

        //    model.Entry(om).State = EntityState.Modified;
        //    model.SaveChanges();

        //    wallet wl = model.wallets.Where(m => m.uid == om.uid).SingleOrDefault();

        //    wl.amount = wl.amount + om.Subtotal;

        //    model.Entry(wl).State = EntityState.Modified;
        //    model.SaveChanges();

        //    return RedirectToAction("orderdetail", "DealerOrders", new { id = ordermasterid });
        //}

        public ActionResult testrefundbtn(string amount, string id)
        {
            long ordermasterid = Convert.ToInt32(id);

            ordermaster om = model.ordermasters.Where(m => m.id == ordermasterid).SingleOrDefault();

            om.OrderStatus = 6;

            model.Entry(om).State = EntityState.Modified;
            model.SaveChanges();

            wallet wl = model.wallets.Where(m => m.uid == om.uid).SingleOrDefault();

            wl.amount = wl.amount + Convert.ToInt32(amount);

            model.Entry(wl).State = EntityState.Modified;
            model.SaveChanges();

            return RedirectToAction("orderdetail", "DealerOrders", new { id = ordermasterid });
        }
    }
}