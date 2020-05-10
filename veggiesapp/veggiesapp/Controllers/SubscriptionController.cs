using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class SubscriptionController : Controller
    {
        // GET: Subscription
        public ActionResult addsubscription()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            return View(new subscription());
        }

        public ActionResult viewsubscription()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.subscriptions.OrderByDescending(m => m.addeddate).ToList());
            }
        }

        public ActionResult addintersubscription(subscription m)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            
            using (sabziappEntities model = new sabziappEntities())
            {
                model.subscriptions.Add(m);
                model.SaveChanges();
            }

            return RedirectToAction("viewsubscription","Subscription");
        }

        public ActionResult editsubscription(string id)
        {

            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.subscriptions.Where(m => m.id == id1).FirstOrDefault());
            }
        }

        public ActionResult updatesubscription(subscription sb)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                model.Entry(sb).State = EntityState.Modified;
                model.SaveChanges();
            }

            return RedirectToAction("viewsubscription","Subscription");
        }

        public ActionResult deletesubscription(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                subscription sub = model.subscriptions.Where(m => m.id == id1).FirstOrDefault();
                model.subscriptions.Remove(sub);
                model.SaveChanges();
            }

            return RedirectToAction("viewsubscription", "Subscription");
        }
    }
}