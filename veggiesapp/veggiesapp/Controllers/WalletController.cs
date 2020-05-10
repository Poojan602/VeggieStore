using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;
using System.Data.Entity;

namespace veggiesapp.Controllers
{
    public class WalletController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult addamount(long id)
        {
            wallet wl = model.wallets.Where(m => m.uid == id).SingleOrDefault();

            if (TempData["message"] != null)
            {
                ViewBag.message = TempData["message"];
            }

            ViewBag.username = model.customers.Where(m => m.id == id).Select(m => m.Name).SingleOrDefault();

            return View(wl);
        }
        
        public ActionResult addinteramount(wallet wl)
        {
            wallet objwl = model.wallets.Where(m => m.uid == wl.uid).SingleOrDefault();

            objwl.amount = objwl.amount + wl.addamount;
            objwl.modifydate = DateTime.Now;

            model.Entry(objwl).State = EntityState.Modified;
            model.SaveChanges();

            TempData["message"] = "Amount added in Wallet";

            return RedirectToAction("addamount","Wallet",new { id = wl.uid });
        }

        public ActionResult adddlramount(long id)
        {
            wallet wl = model.wallets.Where(m => m.uid == id).SingleOrDefault();

            if (TempData["message"] != null)
            {
                ViewBag.message = TempData["message"];
            }

            ViewBag.username = model.customers.Where(m => m.id == id).Select(m => m.Name).SingleOrDefault();

            return View(wl);
        }

        public ActionResult adddlrinteramount(wallet wl)
        {
            wallet objwl = model.wallets.Where(m => m.uid == wl.uid).SingleOrDefault();

            objwl.amount = objwl.amount + wl.addamount;
            objwl.modifydate = DateTime.Now;

            model.Entry(objwl).State = EntityState.Modified;
            model.SaveChanges();

            TempData["message"] = "Amount added in Wallet";

            return RedirectToAction("adddlramount", "Wallet", new { id = wl.uid });
        }
    }
}