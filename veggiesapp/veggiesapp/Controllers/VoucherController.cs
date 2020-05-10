using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;

namespace veggiesapp.Controllers
{
    public class VoucherController : Controller
    {
        public ActionResult generatevoucher()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            using (sabziappEntities model = new sabziappEntities())
            {
                ViewBag.cities = model.cities.ToList();
                ViewBag.ctname1 = TempData["cityname"];
                voucher vc = new voucher();
                if (TempData["area"] != null)
                {
                    vc = (voucher)TempData["area"];
                }
                TempData["path"] = @"/Veggiestore/Voucher/loadareas?cityid=";  
                return View(vc);
            }
        }

        //[HttpPost]
        public ActionResult generateintervoucher(voucher vc)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            using (sabziappEntities model = new sabziappEntities())
            {
                for (int i = 0; i < vc.check.Count; i++)
                {
                    voucher advc = new voucher();

                    if (vc.check[i].ischecked == true)
                    {
                        advc.areaid = vc.check[i].areaid;
                        advc.AdminNote = vc.AdminNote;
                        advc.Code = vc.Code;
                        advc.ExpiryDate = vc.ExpiryDate;
                        advc.MinimumAmount = vc.MinimumAmount;
                        advc.PublishDate = vc.PublishDate;
                        //advc.Towhom = vc.Towhom;
                        advc.Type = vc.Type;
                        advc.VoucherAmount = vc.VoucherAmount;

                        model.vouchers.Add(advc);
                        model.SaveChanges();
                    }
                }
                ViewBag.cities = model.cities.ToList();
            }

            return RedirectToAction("viewvoucher", "Voucher");
        }

        public ActionResult loadareas(string cityid)
        {
            sabziappEntities model = new sabziappEntities();
            List<checkbox> listcheckbox = new List<checkbox>();
            area objarea = new area();
            voucher vc = new voucher();
            List<area> list = new List<area>();
            int id = Convert.ToInt32(cityid);

            if (cityid == "-1")
            {

            }
            else
            {
                List<areaselected> ars = model.areaselecteds.Where(m => m.dealerid != null).ToList();
                area a = new area();

                foreach (var item in ars)
                {
                    long? areaid = item.areaid;
                    a = model.areas.Where(m => m.id == areaid && m.cityid == id && m.visible == true).SingleOrDefault();

                    if (a != null)
                    {
                        list.Add(a);
                    }
                }
                
                //list = model.areas.Where(m => m.cityid == id && m.visible == true).ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    listcheckbox.Add(new checkbox() { areaid = list[i].id, areaname = list[i].Name, ischecked = false });
                }
                vc.check = listcheckbox;
                TempData["area"] = vc;

                var ctid = model.cities.Where(m => m.id == id).FirstOrDefault();
                TempData["cityname"] = ctid.Name;
            }

            return RedirectToAction("generatevoucher");
        }

        public ActionResult viewvoucher()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                IEnumerable<voucherareajoin> voucherarea;

                voucherareajoin objvcar = new voucherareajoin();

                voucherarea = (from ar in model.areas
                               join vc in model.vouchers on ar.id equals vc.areaid
                               select new voucherareajoin
                               {
                                   objar = ar,
                                   objvc = vc,
                               });

                return View(voucherarea.OrderByDescending(m => m.objvc.addeddate).ToList());
            }
        }

        public ActionResult voucherdetails(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                long id1 = (long)Convert.ToDouble(id);
                return View(model.vouchers.Where(m => m.id == id1).FirstOrDefault());
            }
        }

        public ActionResult editvoucher(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }
            
            long id1 = (long)Convert.ToDouble(id);
            
            using (sabziappEntities model = new sabziappEntities())
            {
                var vc = model.vouchers.Where(m => m.id == id1).FirstOrDefault();
                long? areaid = vc.areaid;
                long arid = Convert.ToInt64(areaid);
                var areaname = model.areas.Where(m => m.id == arid).FirstOrDefault();
                ViewBag.AreaName = areaname.Name;

                return View(model.vouchers.Where(m => m.id == id1).FirstOrDefault());
            }
        }

        public ActionResult updatevoucher(voucher vc)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            using (sabziappEntities model = new sabziappEntities())
            {
                model.Entry(vc).State = EntityState.Modified;
                model.SaveChanges();
            }

            return RedirectToAction("viewvoucher", "Voucher");
        }

        public ActionResult deletevoucher(string id)
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            long id1 = (long)Convert.ToDouble(id);

            using (sabziappEntities model = new sabziappEntities())
            {
                voucher vc = model.vouchers.Where(m => m.id == id1).FirstOrDefault();
                model.vouchers.Remove(vc);
                model.SaveChanges();
            }

            return RedirectToAction("viewvoucher", "Voucher");
        }
    }
}