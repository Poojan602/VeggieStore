using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class BannerController : Controller
    {
        sabziappEntities model = new sabziappEntities();
        // GET: Banner
        public ActionResult addbanner()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            return View(new homebanner());
        }

        public ActionResult addinterbanner(homebanner hb, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            //string strpath = Path.GetExtension(postedFile.FileName);
            //if (strpath != ".jpg" && strpath != ".jpeg" && strpath != ".png")
            //{

            //}

            string Name = "";

            if (postedFile.ContentLength > 0)
            {
                Guid Guid = System.Guid.NewGuid();
                Name = Path.GetFileName(postedFile.FileName);

                //string path = Server.MapPath("/Uploads/Banner/");
                //string path = ConfigurationManager.AppSettings["CMSPath"].ToString() + "Uploads/Banner/";

                postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Banner\\" + Name);
            }

            homebanner objDl = new homebanner();
            objDl.visible = hb.visible;
            objDl.image = Name;
            objDl.link = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Banner\\" + Name;
            objDl.addeddate = DateTime.Now;
            objDl.addedip = DAL.getIP(); //Request.UserHostAddress;
            model.Entry(objDl).State = EntityState.Added;
            model.SaveChanges();

            return RedirectToAction("viewbanner", "Banner");
        }

        public ActionResult viewbanner()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            return View(model.homebanners.Where(m => m.dealerid == null).OrderByDescending(m => m.addeddate).ToList());
        }

        public ActionResult editbanner(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            homebanner objPc1 = model.homebanners.Where(m => m.id == id1).SingleOrDefault();
            if (objPc1 != null)
            {
                TempData["image"] = objPc1.image;
                TempData["imagepath"] = objPc1.imagepath;
            }
            return View(objPc1);

        }

        public ActionResult updatebanner(homebanner hb, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            if (postedFile == null)
            {
                if (TempData["image"] != null)
                {
                    hb.image = TempData["image"].ToString();
                    hb.link = TempData["imagepath"].ToString();
                    hb.modifydate = DateTime.Now;
                    hb.modifyip = DAL.getIP();
                }
                else
                {
                    hb.image = "noimage.jpg";
                }

                model.Entry(hb).State = EntityState.Modified;
                model.SaveChanges();
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

                homebanner objDl = new homebanner();
                objDl.image = Name;
                objDl.visible = hb.visible;
                objDl.id = hb.id;
                objDl.link = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Banner\\" + Name; //DAL.GetRootPath() + "\\Uploads\\Banner\\" + Name ;
                model.Entry(objDl).State = EntityState.Modified;
                model.SaveChanges();
            }

            return RedirectToAction("viewbanner", "Banner");
        }

        public ActionResult deletebanner(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            homebanner hb = model.homebanners.Where(m => m.id == id1).FirstOrDefault();
            model.homebanners.Remove(hb);
            model.SaveChanges();

            return RedirectToAction("viewbanner", "Banner");
        }
    }
}