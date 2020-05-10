using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class AdminsettingController : Controller
    {
        sabziappEntities model = new sabziappEntities();
        
        public ActionResult Account()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            
            admin adm = model.admins.FirstOrDefault();

            return View(adm);
        }

        public ActionResult Updateaccountinfo(admin admin)
        {
            admin adm = model.admins.Where(m => m.ID == admin.ID).FirstOrDefault();
            
            if(adm.pwd != admin.oldpass)
            {
                //throw error
            }

            adm.unm = admin.unm;
            adm.pwd = admin.newpass;
            adm.Email = admin.Email;

            model.Entry(adm).State = EntityState.Modified;
            model.SaveChanges();

            return RedirectToAction("Account","Adminsetting");
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult Updatesetting()
        {
            return View();
        }
    }
}