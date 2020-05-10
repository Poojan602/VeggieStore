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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult addcategory1()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            return View(new productcategory1());
        }

        public ActionResult addintercategory1(productcategory1 cat1, HttpPostedFileBase postedFile)
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
                postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Product\\Category1\\" + Name);
            }

            using (sabziappEntities obj = new sabziappEntities())
            {
                productcategory1 objDl = new productcategory1();
                objDl.visible = cat1.visible;
                objDl.image = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Product\\Category1\\" + Name; 
                objDl.name = cat1.name;
                obj.Entry(objDl).State = EntityState.Added;
                obj.SaveChanges();
            }

            return RedirectToAction("viewcategory1", "Product");

        }

        public ActionResult viewcategory1()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                return View(model.productcategory1.OrderByDescending(m => m.addeddate).ToList());
            }
        }

        public ActionResult editcategory1(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                productcategory1 objPc1 = model.productcategory1.Where(m => m.id == id1).SingleOrDefault();
                if (objPc1 != null)
                {
                    TempData["image"] = objPc1.image;
                }
                return View(objPc1);
            }
        }

        public ActionResult updatecategory1(productcategory1 cat1, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            if(postedFile == null)
            {
                if(TempData["image"] != null)
                {
                    cat1.image = TempData["image"].ToString();
                }
                else
                {
                    cat1.image = "noimage.jpg";
                }

                using (sabziappEntities obj = new sabziappEntities())
                {
                    obj.Entry(cat1).State = EntityState.Modified;
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
                    postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Product\\Category1\\" + Name);
                }

                using (sabziappEntities obj = new sabziappEntities())
                {
                    productcategory1 objDl = new productcategory1();
                    objDl.visible = cat1.visible;
                    objDl.image = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Product\\Category1\\" + Name; ;
                    objDl.name = cat1.name;
                    objDl.id = cat1.id;
                    obj.Entry(objDl).State = EntityState.Modified;
                    obj.SaveChanges();
                }
            }
            
            return RedirectToAction("viewcategory1", "Product");
        }

        public ActionResult deletecategory1(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                productcategory1 cat1 = model.productcategory1.Where(m => m.id == id1).FirstOrDefault();
                model.productcategory1.Remove(cat1);
                model.SaveChanges();
            }

            return RedirectToAction("viewcategory1", "Product");
        }

        public ActionResult addcategory2()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            sabziappEntities db = new sabziappEntities();

            ViewBag.cat1names = db.productcategory1.ToList();

            return View(new productcategory2());
        }

        public ActionResult addintercategory2(productcategory2 cat2, HttpPostedFileBase postedFile)
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
                postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Product\\Category2\\" + Name);
            }

            using (sabziappEntities obj = new sabziappEntities())
            {
                productcategory2 objDl = new productcategory2();
                objDl.visible = cat2.visible;
                objDl.image = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Product\\Category2\\" + Name; ;
                objDl.cat1id = cat2.cat1id;
                objDl.name = cat2.name;
                obj.Entry(objDl).State = EntityState.Added;
                obj.SaveChanges();
            }

            return RedirectToAction("viewcategory2", "Product");

        }

        public ActionResult viewcategory2()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {

                IEnumerable<cat1cat2join> cat1cat2;

                cat1cat2join objctar = new cat1cat2join();

                cat1cat2 = (from ct1 in model.productcategory1
                            join ct2 in model.productcategory2 on ct1.id equals ct2.cat1id
                            select new cat1cat2join
                            {
                                objcat1 = ct1,
                                objcat2 = ct2
                            });

                return View(cat1cat2.OrderByDescending(m => m.objcat2.addeddate).ToList());
            }
        }

        public ActionResult editcategory2(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                productcategory2 objPc2 = model.productcategory2.Where(m => m.id == id1).SingleOrDefault();
                if (objPc2 != null)
                {
                    TempData["image"] = objPc2.image;
                }
                
                ViewBag.cat1names = model.productcategory1.ToList();
                
                return View(objPc2);
            }
        }

        public ActionResult updatecategory2(productcategory2 cat2, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            if (postedFile == null)
            {
                if (TempData["image"] != null)
                {
                    cat2.image = TempData["image"].ToString();
                }
                else
                {
                    cat2.image = "noimage.jpg";
                }

                using (sabziappEntities model = new sabziappEntities())
                {
                    model.Entry(cat2).State = EntityState.Modified;
                    model.SaveChanges();
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
                    postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Product\\Category2\\" + Name);
                }

                using (sabziappEntities obj = new sabziappEntities())
                {
                    productcategory2 objDl = new productcategory2();
                    objDl.visible = cat2.visible;
                    objDl.image = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Product\\Category2\\" + Name; ;
                    objDl.name = cat2.name;
                    objDl.id = cat2.id;
                    objDl.cat1id = cat2.cat1id;
                    obj.Entry(objDl).State = EntityState.Modified;
                    obj.SaveChanges();
                }
            }
            
            return RedirectToAction("viewcategory2", "Product");
        }

        public ActionResult deletecategory2(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                productcategory2 cat2 = model.productcategory2.Where(m => m.id == id1).SingleOrDefault();
                model.productcategory2.Remove(cat2);
                model.SaveChanges();
            }

            return RedirectToAction("viewcategory2", "Product");
        }

        public ActionResult addproduct()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            product objproduct = new product();

            using (sabziappEntities db = new sabziappEntities())
            {
                ViewBag.cat1names = db.productcategory1.ToList(); // category 1 list
                ViewBag.cat2names = TempData["cat2list"];   // category 2 list according to category 1 id
                //TempData["cat1name"] = TempData["category1name"]; 
                ViewBag.cat1name = TempData["category1name"];  // category 1 name
                objproduct.cat1id =  Convert.ToInt32(TempData["cat1id"]);
                //TempData["path"] = @"/Veggiestore/Product/loadcat2?cat1id=";
                TempData["path"] = @"loadcat2?cat1id=";
                return View(objproduct);
            }          
        }
        

        public ActionResult addinterproduct(product pr, HttpPostedFileBase postedFile)
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
                postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Product\\product\\" + Name);
            }

            using (sabziappEntities obj = new sabziappEntities())
            {
                product objpr = new product();
                objpr.visible = pr.visible;
                objpr.featured = pr.featured;
                objpr.Image = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Product\\product\\" + Name;
                objpr.Title = pr.Title;
                objpr.HindiTitle = pr.HindiTitle;
                objpr.cat1id = pr.cat1id;
                objpr.cat2id = pr.cat2id;
                objpr.DESCabout = pr.DESCabout;
                objpr.DESCbenifits = pr.DESCbenifits;
                objpr.DESChowtouse = pr.DESChowtouse;
                objpr.addeddate = DateTime.Now;
                obj.Entry(objpr).State = EntityState.Added;
                obj.SaveChanges();
            }

            return RedirectToAction("viewproduct", "Product");

        }

        public ActionResult loadcat2(string cat1id)
        {
            sabziappEntities model = new sabziappEntities();
            
            if(cat1id == "-1")
            {

            }
            else
            {
                List<productcategory2> list = new List<productcategory2>();
                int id = Convert.ToInt32(cat1id);

                list = model.productcategory2.Where(m => m.cat1id == id).ToList();

                TempData["cat2list"] = list;
                TempData["cat1id"] = cat1id;
                var cat1 = model.productcategory1.Where(m => m.id == id).FirstOrDefault();
                TempData["category1name"] = cat1.name;
            }
           
            return RedirectToAction("addproduct");
        }
        
        
        public ActionResult editproduct(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                product objPr = model.products.Where(m => m.id == id1).SingleOrDefault();

                if (objPr != null)
                {
                    TempData["image"] = objPr.Image;
                }
                
                var catid1 = objPr.cat1id;
                var c1 = model.productcategory1.Where(m => m.id == catid1).FirstOrDefault();
                
                var catid2 = objPr.cat2id;
                var c2 = model.productcategory2.Where(m => m.id == catid2).FirstOrDefault();
                
                ViewBag.cat1name = c1.name;
                ViewBag.cat2name = c2.name;
                ViewBag.cat2id = catid2;

                var cat2list = model.productcategory2.Where(m => m.cat1id == c1.id).ToList();
                ViewBag.cat2list = cat2list;

                return View(objPr);
            }
        }

        public ActionResult updateproduct(product pr, HttpPostedFileBase postedFile)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            if (postedFile == null)
            {
                if (TempData["image"] != null)
                {
                    pr.Image = TempData["image"].ToString();
                }
                else
                {
                    pr.Image = "noimage.jpg";
                }

                using (sabziappEntities model = new sabziappEntities())
                {
                    model.Entry(pr).State = EntityState.Modified;
                    model.SaveChanges();
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
                    postedFile.SaveAs(DAL.GetRootPath() + "\\Uploads\\Product\\product\\" + Name);
                }

                using (sabziappEntities obj = new sabziappEntities())
                {
                    product objDl = new product();
                    objDl.id = pr.id;
                    objDl.cat1id = pr.cat1id;
                    objDl.cat2id = pr.cat2id;
                    objDl.visible = pr.visible;
                    objDl.Image = ConfigurationManager.AppSettings["CMSPath"].ToString() + "\\Uploads\\Product\\product\\" + Name; ;
                    objDl.featured = pr.featured;
                    objDl.DESCabout = pr.DESCabout;
                    objDl.DESCbenifits = pr.DESCbenifits;
                    objDl.DESChowtouse = pr.DESChowtouse;
                    objDl.HindiTitle = pr.HindiTitle;
                    objDl.Title = pr.Title;
                    obj.Entry(objDl).State = EntityState.Modified;
                    obj.SaveChanges();
                }
            }
            return RedirectToAction("viewproduct", "Product");
        }

        public ActionResult deleteproduct(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                product pr = model.products.Where(m => m.id == id1).SingleOrDefault();
                model.products.Remove(pr);
                model.SaveChanges();
            }

            return RedirectToAction("viewproduct","Product");
        }

        public ActionResult viewproduct()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            sabziappEntities model = new sabziappEntities();

            return View(model.products.Where(m => m.dealerid == null).OrderByDescending(m => m.addeddate).ToList());
        }
      
    }
}