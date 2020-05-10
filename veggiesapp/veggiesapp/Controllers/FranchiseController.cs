using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class FranchiseController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult ViewEnquiry()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            List<enquiry> en = model.enquiries.OrderByDescending(m => m.addeddate).ToList();

            return View(en);
        }

        public ActionResult EnquiryDetails(long id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            enquiry en = model.enquiries.Where(m => m.id == id).SingleOrDefault();

            return View(en);
        }

        public ActionResult DeleteEnquiry(long id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            enquiry en = model.enquiries.Where(m => m.id == id).SingleOrDefault();
            model.enquiries.Remove(en);
            model.SaveChanges();

            return RedirectToAction("ViewEnquiry", "Franchise");
        }
    }
}