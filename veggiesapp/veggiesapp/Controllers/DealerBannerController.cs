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
    public class DealerBannerController : Controller
    {
        public ActionResult addbanner()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }
            homebanner bnr = new homebanner();
            bnr.dealerid = Convert.ToInt32(Session["ID"]);

            return View(bnr);
        }

        public ActionResult addinterbanner(homebanner hb, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            //string strpath = Path.GetExtension(postedFile.FileName);
            //if (strpath != ".doc" && strpath != ".docx" && strpath != ".pdf" && strpath != ".txt")
            //{

            //}
            string Name = "";

            if (postedFile.ContentLength > 0)
            {
                Guid Guid = System.Guid.NewGuid();
                Name = Path.GetFileName(postedFile.FileName);

                postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Banner\\" + Name);
            }

            using (sabziappEntities obj = new sabziappEntities())
            {
                homebanner objDl = new homebanner();
                objDl.visible = hb.visible;
                objDl.image = Name;
                objDl.dealerid = hb.dealerid;
                objDl.link = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Banner\\" + Name;
                obj.Entry(objDl).State = EntityState.Added;
                obj.SaveChanges();
            }

            return RedirectToAction("viewbanner", "DealerBanner");
        }

        public ActionResult viewbanner()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.homebanners.Where(m => m.dealerid == dlrid).OrderByDescending(m => m.addeddate).ToList());
            }
        }

        public ActionResult editbanner(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                homebanner objPc1 = model.homebanners.Where(m => m.id == id1).SingleOrDefault();
                if (objPc1 != null)
                {
                    TempData["image"] = objPc1.image;
                    TempData["imagepath"] = objPc1.imagepath;
                }
                return View(objPc1);
            }
        }

        public ActionResult updatebanner(homebanner hb, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            if (postedFile == null)
            {
                if (TempData["image"] != null)
                {
                    hb.image = TempData["image"].ToString();
                    hb.link = TempData["imagepath"].ToString();
                }
                else
                {
                    hb.image = "noimage.jpg";
                }

                using (sabziappEntities obj = new sabziappEntities())
                {
                    obj.Entry(hb).State = EntityState.Modified;
                    obj.SaveChanges();
                }
            }
            else
            {
                string strpath = Path.GetExtension(postedFile.FileName);
                string Name = "";

                if (postedFile.ContentLength > 0)
                {
                    Guid Guid = System.Guid.NewGuid();
                    Name = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Banner\\" + Name);
                }

                using (sabziappEntities obj = new sabziappEntities())
                {
                    homebanner objDl = new homebanner();
                    objDl.image = Name;
                    objDl.visible = hb.visible;
                    objDl.dealerid = hb.dealerid;
                    objDl.id = hb.id;
                    objDl.link = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Banner\\" + Name; //DAL.GetRootPath() + "\\Uploads\\Banner\\" + Name ;
                    obj.Entry(objDl).State = EntityState.Modified;
                    obj.SaveChanges();
                }
            }

            return RedirectToAction("viewbanner", "DealerBanner");
        }

        public ActionResult deletebanner(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                homebanner hb = model.homebanners.Where(m => m.id == id1).FirstOrDefault();
                model.homebanners.Remove(hb);
                model.SaveChanges();
            }

            return RedirectToAction("viewbanner", "DealerBanner");
        }
    }
}