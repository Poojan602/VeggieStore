using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace veggiesapp.Controllers
{
    public class DealerTicketController : Controller
    {
        // GET: DealerTicket
        public ActionResult support()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }
            return View();
        }
    }
}