using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class DealerNotificationController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult addnotification()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            return View(new notification());
        }

        public ActionResult addinternotification(notification noti, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            string strpath = Path.GetExtension(postedFile.FileName);
            if (strpath != ".doc" && strpath != ".docx" && strpath != ".pdf" && strpath != ".txt")
            {

            }
            string Name = "";

            if (postedFile.ContentLength > 0)
            {
                Guid Guid = System.Guid.NewGuid();
                Name = Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Notification\\" + Name);
            }

            notification ntfc = new notification();

            ntfc.dealerid = Convert.ToInt32(Session["ID"]);
            ntfc.image = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Notification\\" + Name; ;
            ntfc.Message = noti.Message;
            ntfc.addeddate = DateTime.Now;

            model.notifications.Add(ntfc);
            model.SaveChanges();

            return RedirectToAction("viewnotification", "DealerNotification");
        }

        public ActionResult viewnotification()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            IEnumerable<Notificationdealerjoin> notificationdealer;

            long dlrid = Convert.ToInt32(Session["ID"]);
            Notificationdealerjoin objnotidealer = new Notificationdealerjoin();

            notificationdealer = (from dlr in model.dealers
                                  join noti in model.notifications.Where(m => m.dealerid == dlrid) on dlr.id equals noti.dealerid
                                  select new Notificationdealerjoin
                                  {
                                      objnoti = noti,
                                      objdlr = dlr,
                                  }).OrderByDescending(m => m.objnoti.addeddate).ToList();

            return View(notificationdealer.ToList());
        }

        public ActionResult notificationdetail(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long id1 = (long)Convert.ToDouble(id);

            return View(model.notifications.Where(m => m.id == id1).FirstOrDefault());
        }

        public ActionResult deletenotification(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long id1 = (long)Convert.ToDouble(id);

            notification noti = model.notifications.Where(m => m.id == id1).FirstOrDefault();
            model.notifications.Remove(noti);
            model.SaveChanges();

            return RedirectToAction("viewnotification", "DealerNotification");
        }
    }
}