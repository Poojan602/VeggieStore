using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult addnotification()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                ViewBag.cities = model.cities.ToList();
                ViewBag.ctname1 = TempData["cityname"];
                ViewBag.cityid = TempData["cityid"];
                notification noti = new notification();
                if (TempData["area"] != null)
                {
                    noti = (notification)TempData["area"];
                }
                TempData["path"] = @"/Veggiestore/Notification/loadareas?cityid=";
                //TempData["path"] = @"/Notification/loadareas?cityid=";
                return View(noti);
            }
        }

        public ActionResult loadareas(string cityid)
        {
            sabziappEntities model = new sabziappEntities();

            List<checkboxnotification> listcheckbox = new List<checkboxnotification>();
            area objarea = new area();
            notification hld = new notification();
            List<area> list = new List<area>();
            int id = Convert.ToInt32(cityid);

            if (id == -1)
            {
                TempData["cityname"] = null;
                TempData["cityid"] = null;
            }
            else
            {
                List<areaselected> ars = model.areaselecteds.Where(m => m.dealerid != null).ToList();

                foreach (var item in ars)
                {
                    area a = new area();
                    a = model.areas.Where(m => m.id == item.areaid && m.cityid == id && m.visible == true).FirstOrDefault();

                    if (a != null)
                    {
                        list.Add(a);
                    }
                }

                checkboxnotification objcheckboc = new checkboxnotification();
                for (int i = 0; i < list.Count; i++)
                {
                    listcheckbox.Add(new checkboxnotification() { aid = list[i].id, aname = list[i].Name, ticked = false });
                }
                hld.check = listcheckbox;
                TempData["area"] = hld;

                var ctid = model.cities.Where(m => m.id == id).FirstOrDefault();
                TempData["cityname"] = ctid.Name;
                TempData["cityid"] = id;
            }
            
            return RedirectToAction("addnotification");
        }

        public ActionResult addinternotification(notification noti, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
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

            using (sabziappEntities model = new sabziappEntities())
            {
                for (int i = 0; i < noti.check.Count; i++)
                {
                    notification ntfc = new notification();

                    if (noti.check[i].ticked == true)
                    {
                        var arid = noti.check[i].aid;
                        var dlrid = model.areaselecteds.Where(m => m.areaid == arid).FirstOrDefault();
                        ntfc.dealerid = dlrid.dealerid;
                        ntfc.image = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Notification\\" + Name; 
                        ntfc.Message = noti.Message;
                        ntfc.addeddate = DateTime.Now;

                        model.notifications.Add(ntfc);
                        model.SaveChanges();
                    }
                }
                ViewBag.cities = model.cities.ToList();
            }
            
            return RedirectToAction("viewnotification", "Notification");
        }

        public ActionResult viewnotification()
        {
            using (sabziappEntities model = new sabziappEntities())
            {
                IEnumerable<Notificationdealerjoin> notificationdealer;

                Notificationdealerjoin objnotidealer = new Notificationdealerjoin();

                notificationdealer = (from dlr in model.dealers
                                 join noti in model.notifications on dlr.id equals noti.dealerid
                                 select new Notificationdealerjoin
                                 {
                                     objnoti = noti,
                                     objdlr = dlr,
                                 }).OrderByDescending(m => m.objnoti.addeddate).ToList();

                return View(notificationdealer);
            }
        }

        public ActionResult notificationdetail(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.notifications.Where(m => m.id == id1).FirstOrDefault());
            }
        }

        public ActionResult deletenotification(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                notification noti = model.notifications.Where(m => m.id == id1).FirstOrDefault();
                model.notifications.Remove(noti);
                model.SaveChanges();
            }
            return RedirectToAction("viewnotification", "Notification");
        }
    }
}