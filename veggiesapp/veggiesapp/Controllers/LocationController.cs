using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class LocationController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult addcity()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            
            return View();
        }

        public ActionResult addintercity(city c)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                model.cities.Add(c);
                model.SaveChanges();
            }

            return RedirectToAction("viewcity", "Location");
        }

        public ActionResult viewcity()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            if (TempData["error"] != null)
            {
                ViewBag.error = TempData["error"];
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.cities.OrderByDescending(m => m.addeddate).ToList());
            }
        }

        public ActionResult editcity(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.cities.Where(m => m.id == id1).FirstOrDefault());
            }

        }

        public ActionResult updatecity(city c)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                model.Entry(c).State = EntityState.Modified;
                model.SaveChanges();
            }

            return RedirectToAction("viewcity", "Location");
        }

        public ActionResult deletecity(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            List<area> a = model.areas.Where(m => m.cityid == id1).ToList();

            if (a != null)
            {
                TempData["error"] = "Cannot delete because of dependencies";
                return RedirectToAction("viewcity", "Location");
            }

            city ct = model.cities.Where(m => m.id == id1).FirstOrDefault();
            model.cities.Remove(ct);
            model.SaveChanges();

            return RedirectToAction("viewcity", "Location");
        }

        public ActionResult addarea()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            sabziappEntities db = new sabziappEntities();

            ViewBag.citynames = db.cities.ToList();

            return View();
        }

        public ActionResult addinterarea(area a)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            sabziappEntities model = new sabziappEntities();

            model.areas.Add(a);
            model.SaveChanges();

            area ar = model.areas.Where(m => m.Name == a.Name).FirstOrDefault();
            areaselected ars = new areaselected();
            ars.areaid = ar.id;

            model.areaselecteds.Add(ars);
            model.SaveChanges();

            return RedirectToAction("viewarea", "Location");
        }

        public ActionResult viewarea()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {

                IEnumerable<cityareajoin> cityarea;

                cityareajoin objctar = new cityareajoin();

                cityarea = (from ct in model.cities
                            join ar in model.areas on ct.id equals ar.cityid
                            select new cityareajoin
                            {
                                objcity = ct,
                                objarea = ar
                            });

                return View(cityarea.OrderByDescending(m => m.objarea.addeddate).ToList());
            }
        }

        public ActionResult editarea(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                ViewBag.citynames = model.cities.ToList();
                return View(model.areas.Where(m => m.id == id1).FirstOrDefault());
            }
        }

        public ActionResult updatearea(area ar)
        {

            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                model.Entry(ar).State = EntityState.Modified;
                model.SaveChanges();
            }

            return RedirectToAction("viewarea", "Location");
        }

        public ActionResult deletearea(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                area ar = model.areas.Where(m => m.id == id1).FirstOrDefault();
                model.areas.Remove(ar);
                model.SaveChanges();
            }

            return RedirectToAction("viewarea", "Location");
        }
    }
}