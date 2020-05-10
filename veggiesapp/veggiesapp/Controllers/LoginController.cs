using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (TempData["error"] != null)
            {
                ViewBag.loginerror = TempData["error"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(admin loginmodel)
        {
            using (sabziappEntities model = new sabziappEntities())
            {
                var userdetails = model.admins.Where(x => x.unm == loginmodel.unm && x.pwd == loginmodel.pwd).FirstOrDefault();

                if (userdetails == null)
                {
                    TempData["error"] = "Wrong Username and/or Password";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    Session["Username"] = loginmodel.unm;
                    Session["Password"] = loginmodel.pwd;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult resetpassword()
        {
            if (TempData["errormessage"] != null)
            {
                ViewBag.errormessage = "Email not found";
            }
            return View();
        }

        public ActionResult resetpass(string email)
        {
            sabziappEntities model = new sabziappEntities();

            dealer dl = model.dealers.Where(m => m.Email == email).SingleOrDefault();

            if (dl == null)
            {
                TempData["errormessage"] = "Email not found";
                return RedirectToAction("resetpassword", "Login");
            }
            else
            {
                MailMessage mailMessage = new MailMessage("patelpoojan602@gmail.com", email);
                mailMessage.Subject = "Password Recovery";
                mailMessage.Body = "Your Password : " + dl.Password;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }

            return RedirectToAction("Index", "Login");
        }
    }
}