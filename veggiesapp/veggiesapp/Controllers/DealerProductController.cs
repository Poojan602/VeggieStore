using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;
using WebGrease.Css.Extensions;

namespace veggiesapp.Controllers
{
    public class DealerProductController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult addproducts()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            if (TempData["check"] != null)
            {
                ViewBag.error = TempData["check"].ToString();
            }

            product objproduct = new product();

            using (sabziappEntities db = new sabziappEntities())
            {
                ViewBag.cat1names = db.productcategory1.ToList(); // category 1 list
                ViewBag.cat2names = TempData["cat2list"];   // category 2 list according to category 1 id
                TempData.Keep("cat2list");
                TempData["cat2list"] = ViewBag.cat2names;
                ViewBag.products = TempData["productlist"]; // product list according to category 2 id
                TempData.Keep("productlist");
                ViewBag.cat1name = TempData["category1name"];  // category 1 name
                TempData.Keep("category1name");
                ViewBag.cat2name = TempData["category2name"];  // category 2 name
                TempData.Keep("category2name");
                ViewBag.pname = TempData["pname"];
                TempData.Keep("pname");
                //objproduct.cat1id = Convert.ToInt32(TempData["cat1id"]);
                TempData["path"] = @"loadcat2?cat1id=";
                TempData["path2"] = @"loadproducts?cat2id=";
                TempData["path3"] = @"loadproductdetails?pid=";

                TempData["cat1id"] = TempData["cat1id"];

                product row = (product)TempData["productrow"];

                if (row != null)
                {
                    objproduct.Title = row.Title;
                    objproduct.cat1id = row.cat1id;
                    objproduct.cat2id = row.cat2id;
                    objproduct.DESCabout = row.DESCabout;
                    objproduct.DESCbenifits = row.DESCbenifits;
                    objproduct.DESChowtouse = row.DESChowtouse;
                    objproduct.HindiTitle = row.HindiTitle;
                    objproduct.Image = row.Image;
                    objproduct.Title = row.Title;
                    objproduct.featured = row.featured;

                    return View(objproduct);
                }
                else
                {
                    return View(objproduct);
                }
            }
        }

        public ActionResult loadcat2(string cat1id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            sabziappEntities model = new sabziappEntities();

            List<productcategory2> list = new List<productcategory2>();
            int id = Convert.ToInt32(cat1id);

            if (id == -1)
            {
                TempData["cat1id"] = null;
                TempData["category1name"] = null;
                TempData["cat2list"] = null;
                TempData["productlist"] = null;
                TempData["category2name"] = null;
                TempData["pname"] = null;
                TempData["productrow"] = null;
            }
            else
            {
                TempData["pname"] = null;
                TempData["productrow"] = null;
                TempData["category2name"] = null;

                list = model.productcategory2.Where(m => m.cat1id == id).ToList();

                TempData["cat2list"] = list;
                TempData["cat1id"] = cat1id;

                var cat1 = model.productcategory1.Where(m => m.id == id).FirstOrDefault();
                TempData["category1name"] = cat1.name;
            }

            return RedirectToAction("addproducts");
        }

        public ActionResult loadproducts(string cat2id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            int id = Convert.ToInt32(cat2id);

            if (id == -1)
            {
                TempData["cat2id"] = null;
                TempData["category2name"] = null;
                TempData["productlist"] = null;
                TempData["pname"] = null;
                TempData["productrow"] = null;

            }
            else
            {
                TempData["cat2list"] = TempData["cat2list"];

                sabziappEntities model = new sabziappEntities();

                long cat1id = Convert.ToInt32(TempData["cat1id"]);
                List<product> list = new List<product>();
                

                list = model.products.Where(m => m.cat2id == id && m.cat1id == cat1id && m.dealerid == null).ToList();

                TempData["productlist"] = list;
                TempData["cat2id"] = cat2id;

                var cat2 = model.productcategory2.Where(m => m.id == id).FirstOrDefault();
                TempData["category2name"] = cat2.name;
            }

            return RedirectToAction("addproducts");
        }

        public ActionResult loadproductdetails(string pid)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            sabziappEntities model = new sabziappEntities();

            int id = Convert.ToInt32(pid);

            if (id == -1)
            {
                TempData["pname"] = null;
                TempData["productrow"] = null;
            }
            else
            {
                product productrow = new product();

                productrow = model.products.Where(m => m.id == id).FirstOrDefault();

                TempData["productrow"] = productrow;
                TempData["pname"] = productrow.Title;
            }

            return RedirectToAction("addproducts");
        }

        public ActionResult addinterproducts(product pr)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dlrid = Convert.ToInt32(Session["ID"]);

            product checkpr = model.products.Where(m => m.Title == pr.Title && m.dealerid == dlrid).SingleOrDefault();

            if (checkpr != null)
            {
                TempData["check"] = "Product Already Added";

                TempData["cat1id"] = null;
                TempData["category1name"] = null;
                TempData["cat2list"] = null;
                TempData["cat2id"] = null;
                TempData["category2name"] = null;
                TempData["productlist"] = null;
                TempData["pname"] = null;
                TempData["productrow"] = null;

                return RedirectToAction("addproducts","DealerProduct");
            }
            using (sabziappEntities obj = new sabziappEntities())
            {
                product objpr = new product();
                objpr.visible = pr.visible;
                objpr.featured = pr.featured;
                objpr.Title = pr.Title;
                objpr.HindiTitle = pr.HindiTitle;
                objpr.cat1id = pr.cat1id;
                objpr.cat2id = pr.cat2id;
                objpr.DESCabout = pr.DESCabout;
                objpr.DESCbenifits = pr.DESCbenifits;
                objpr.DESChowtouse = pr.DESChowtouse;
                objpr.Image = pr.Image;
                objpr.featured = pr.featured;
                objpr.dealerid = Convert.ToInt32(Session["ID"]);
                objpr.addeddate = DateTime.Now;
                obj.Entry(objpr).State = EntityState.Added;

                obj.SaveChanges();
            }
            
            TempData["cat1id"] = null;
            TempData["category1name"] = null;
            TempData["cat2list"] = null;
            TempData["cat2id"] = null;
            TempData["category2name"] = null;
            TempData["productlist"] = null;
            TempData["pname"] = null;
            TempData["productrow"] = null;
            
            return RedirectToAction("viewproducts", "DealerProduct");
        }

        public ActionResult prices(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            int pid = Convert.ToInt32(id);

            List<pricemanager> pm = model.pricemanagers.Where(m => m.productid == pid).ToList();

            pricemanager objpricemanager = new pricemanager();
            if (pm.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    objpricemanager.productid = pid;
                    model.Entry(objpricemanager).State = EntityState.Added;
                    model.SaveChanges();
                }
                List<pricemanager> fpm = model.pricemanagers.Where(m => m.productid == pid).ToList();
                return View(fpm);
            }
            else
            {
                return View(pm);
            }
        }

        public ActionResult addprices(List<pricemanager> prlst)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            pricemanager pm = new pricemanager();

            foreach (var price in prlst)
            {
                model.Entry(price).State = EntityState.Modified;
                model.SaveChanges();
            }
            return RedirectToAction("viewproducts", "DealerProduct");
        }

        public ActionResult viewproducts()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long id = Convert.ToInt32(Session["ID"]);

            var pr = model.products.Where(m => m.dealerid == id).OrderByDescending(m => m.addeddate).ToList();
            return View(pr);
        }

        public ActionResult productdetail(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long id1 = (long)Convert.ToDouble(id);

            product pr = model.products.Where(m => m.id == id1).FirstOrDefault();
            return View(pr);
        }

        public ActionResult deleteproducts(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long id1 = (long)Convert.ToDouble(id);

            product pr = model.products.Where(m => m.id == id1).FirstOrDefault();
            model.products.Remove(pr);
            model.SaveChanges();

            List<pricemanager> pm = model.pricemanagers.Where(m => m.productid == id1).ToList();
            model.pricemanagers.RemoveRange(pm);
            model.SaveChanges();

            return RedirectToAction("viewproducts", "DealerProduct");
        }
    }
}