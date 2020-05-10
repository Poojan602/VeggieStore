using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class HolidaysController : Controller
    {
        // GET: Holidays
        public ActionResult viewholidays()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                IEnumerable<holidaydealerjoin> holidaydealer;

                holidaydealerjoin objhldl = new holidaydealerjoin();

                holidaydealer = (from dlr in model.dealers
                               join hld in model.holidays on dlr.id equals hld.dealerid
                               select new holidaydealerjoin
                               {
                                   objhld = hld,
                                   objdlr = dlr,
                               });

                return View(holidaydealer.OrderByDescending(m => m.objhld.addeddate).ToList());
            }
        }

        public ActionResult addholidays()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                ViewBag.cities = model.cities.Where(m => m.visible == true).ToList();
                ViewBag.ctname1 = TempData["cityname"];
                ViewBag.ctid1 = TempData["cityid"];
                holiday hld = new holiday();
                if (TempData["area"] != null)
                {
                    hld = (holiday)TempData["area"];
                }
                TempData["path"] = @"/Veggiestore/Holidays/loadareas?cityid=";
                return View(hld);
            }
        }

        public ActionResult loadareas(string cityid)
        {
            sabziappEntities model = new sabziappEntities();
            List<checkboxholiday> listcheckbox = new List<checkboxholiday>();
            area objarea = new area();
            holiday hld = new holiday();
            List<area> list = new List<area>();
            int id = Convert.ToInt32(cityid);

            if (id == -1)
            {

            }
            else
            {
                List<areaselected> ars = model.areaselecteds.Where(m => m.dealerid != null).ToList();
                area a = new area();

                foreach (var item in ars)
                {
                    long? areaid = item.areaid;
                    a = model.areas.Where(m => m.id == areaid && m.cityid == id && m.visible == true).SingleOrDefault();

                    if (a != null)
                    {
                        list.Add(a);
                    }
                }

                //list = model.areas.Where(m => m.cityid == id && m.visible == true).ToList();
                checkboxholiday objcheckboc = new checkboxholiday();
                for (int i = 0; i < list.Count; i++)
                {
                    listcheckbox.Add(new checkboxholiday() { arid = list[i].id, arname = list[i].Name, ischeck = false });
                }
                hld.check = listcheckbox;
                TempData["area"] = hld;

                var ctid = model.cities.Where(m => m.id == id).FirstOrDefault();
                TempData["cityname"] = ctid.Name;
                TempData["cityid"] = id;
            }
            
            return RedirectToAction("addholidays");
        }

        public ActionResult addinterholiday(holiday h)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                for (int i = 0; i < h.check.Count; i++)
                {
                    holiday hld = new holiday();

                    if (h.check[i].ischeck == true)
                    {
                        var aid = h.check[i].arid;
                        var dlrid = model.areaselecteds.Where(m => m.areaid == aid).FirstOrDefault();
                        hld.dealerid = dlrid.dealerid;
                        hld.holidaydate = h.holidaydate;
                        hld.title = h.title;

                        model.holidays.Add(hld);
                        model.SaveChanges();
                    }
                }
                ViewBag.cities = model.cities.ToList();
            }

            return RedirectToAction("viewholidays", "Holidays");
        }

        public ActionResult editholidays(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                var hld = model.holidays.Where(m => m.id == id1).FirstOrDefault();
                long? did = hld.dealerid;
                long dlrid = Convert.ToInt64(did);
                var dlr = model.dealers.Where(m => m.id == dlrid).FirstOrDefault();
                var dlrname = dlr.Name;
                ViewBag.dealername = dlrname;

                return View(model.holidays.Where(m => m.id == id1).FirstOrDefault());
            }
            
        }

        public ActionResult updateholiday(holiday h)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                model.Entry(h).State = EntityState.Modified;
                model.SaveChanges();
            }
            return RedirectToAction("viewholidays", "Holidays");
        }

        public ActionResult deleteholiday(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                holiday hld = model.holidays.Where(m => m.id == id1).FirstOrDefault();
                model.holidays.Remove(hld);
                model.SaveChanges();
            }

            return RedirectToAction("viewholidays", "Holidays");
        }
    }
}