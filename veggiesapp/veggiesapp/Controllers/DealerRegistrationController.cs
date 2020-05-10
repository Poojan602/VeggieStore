using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class DealerRegistrationController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult RegistrationForm()
        {
            if (TempData["error"] != null)
            {
                ViewBag.error = TempData["error"];
            }

            List<city> cities = new List<city>();

            cities = model.cities.ToList();

            ViewBag.cities = new SelectList(cities, "id", "Name");

            dealersubscriptionselected sub = new dealersubscriptionselected();

            sub.subscriptions = model.subscriptions.ToList();
            sub.selectedsubscriptionid = null;

            return View(sub);
        }

        public ActionResult AddDealer(dealersubscriptionselected dlrsub)
        {
            dealer checkdlr = model.dealers.Where(m => m.Email == dlrsub.dealer.Email).SingleOrDefault();
            if (checkdlr != null)
            {
                TempData["error"] = "Email already Registered";
                return RedirectToAction("RegistrationForm", "DealerRegistration");
            }
            dealer dlr = new dealer();
            dlr.Name = dlrsub.dealer.Name;
            dlr.Address = dlrsub.dealer.Address;
            dlr.Landmark = dlrsub.dealer.Landmark;
            dlr.City = dlrsub.dealer.City;
            dlr.PostCode = dlrsub.dealer.PostCode;
            dlr.Email = dlrsub.dealer.Email;
            dlr.Password = dlrsub.dealer.Password;
            dlr.Phone = dlrsub.dealer.Phone;
            dlr.SlotDescription = dlrsub.dealer.SlotDescription;
            dlr.otherarea = dlrsub.dealer.otherarea;
            dlr.Status = true;
            model.dealers.Add(dlr);
            model.SaveChanges();

            string email1 = dlrsub.dealer.Email;
            string pass = dlrsub.dealer.Password;

            var dealerid = model.dealers.Where(m => m.Email == email1 && m.Password == pass).FirstOrDefault();

            subscriptionselected sub = new subscriptionselected();
            long id = Convert.ToInt32(dlrsub.selectedsubscriptionid);
            subscription s = model.subscriptions.Where(m => m.id == id).FirstOrDefault();

            sub.month = s.month;
            sub.name = s.Name;
            sub.days = s.days;
            sub.amount = s.amount;
            sub.dealerid = dealerid.id;
            sub.status = 1;
            sub.enddate = DateTime.Now.AddDays(Convert.ToInt32(s.days));
            model.subscriptionselecteds.Add(sub);
            model.SaveChanges();

            return RedirectToAction("loginpage", "DealerLogin");
        }
        
    }
}