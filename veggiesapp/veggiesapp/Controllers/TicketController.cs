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
    public class TicketController : Controller
    {
        public ActionResult addreason()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            return View(new ticketdetail());
        }

        public ActionResult addinterreason(ticketdetail tc)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                model.ticketdetails.Add(tc);
                model.SaveChanges();
            }
                return RedirectToAction("viewreason", "Ticket");
        }
        
        public ActionResult viewreason()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.ticketdetails.ToList());
            }
        }

        public ActionResult editreason(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.ticketdetails.Where(m => m.id == id1).FirstOrDefault());
            }
        }

        public ActionResult updatereason(ticketdetail tc)
        {
            using (sabziappEntities model = new sabziappEntities())
            {
                model.Entry(tc).State = EntityState.Modified;
                model.SaveChanges();
            }
            return RedirectToAction("viewreason","Ticket");
        }

        public ActionResult deletereason(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                ticketdetail tc = model.ticketdetails.Where(m => m.id == id1).FirstOrDefault();
                model.ticketdetails.Remove(tc);
                model.SaveChanges();
            }
            return RedirectToAction("viewreason", "Ticket");
        }

        public ActionResult viewticket()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            return View(model.tickets.ToList());
        }

        public ActionResult viewticketdetails(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            return View(model.tickets.Where(m => m.id == id1).FirstOrDefault());
        }

        public ActionResult deleteticket(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                ticket tc = model.tickets.Where(m => m.id == id1).FirstOrDefault();
                model.tickets.Remove(tc);
                model.SaveChanges();
            }
            return RedirectToAction("viewticket", "Ticket");
        }

    }
}