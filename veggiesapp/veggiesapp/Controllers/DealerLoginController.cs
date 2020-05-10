using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using veggiesapp.Models;
using System.Data.Entity;

namespace veggiesapp.Controllers
{
    public class DealerLoginController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult Index()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            long dealerid = Convert.ToInt32(Session["ID"]);

            IEnumerable<OrdermasterCustomerjoin> list;

            list = (from orm in model.ordermasters.Where(m => m.dealerid == dealerid)
                    join cust in model.customers on orm.uid equals cust.id
                    select new OrdermasterCustomerjoin
                    {
                        objordmaster = orm,
                        objcustomer = cust
                    }).ToList().OrderByDescending(m => m.objordmaster.Orderdate).Take(5);

            
            List<customer> customerlist = new List<customer>();
            customer objC = new customer();
            List<long?> customerids = model.ordermasters.Where(m => m.dealerid == dealerid).Select(m => m.uid).Distinct().ToList();
            foreach (long item in customerids)
            {
                long? customerid = item;
                objC = model.customers.Where(s => s.id == customerid).SingleOrDefault();
                if (objC != null)
                {
                    customerlist.Add(objC);
                }
            }
            
            long totalcust = customerlist.Count();
            ViewBag.totalcust = totalcust;
            ViewBag.totaldlr = model.dealers.Count();

            List<ordermaster> om = model.ordermasters.Where(m => m.dealerid == dealerid).ToList();

            long total = 0;
            foreach (var item in om)
            {
                total = total + Convert.ToInt32(item.Grandtotal);
            }

            ViewBag.totalrevenue = total;
            if (totalcust == 0)
            {
                ViewBag.avgrevenue = 0;
            }
            else
            {
                ViewBag.avgrevenue = total / totalcust;
            }
            
            ViewBag.returneditems = model.ordermasters.Where(m => m.OrderStatus == 5 && m.dealerid == dealerid).Count();

            List<decimal?> ordm = model.ordermasters.Where(m => m.OrderStatus == 6 && m.dealerid == dealerid).Select(m => m.Grandtotal).ToList();

            decimal? total12 = 0;
            foreach (var item in ordm)
            {
                total12 = total12 + item;
            }
            ViewBag.totalreturnvalue = total12;
            
            return View(list);
        }

        public ActionResult loginpage()
        {
            if (TempData["error"] != null)
            {
                ViewBag.loginerror = TempData["error"];
            }
            return View();
        }

        public ActionResult login(string id)
        {
            long id1 = Convert.ToInt32(id);
            dealer dealerdetail = model.dealers.Where(s => s.id == id1).FirstOrDefault();
            Session["Username"] = dealerdetail.Name;
            Session["Password"] = dealerdetail.Password;
            Session["ID"] = dealerdetail.id;
            return RedirectToAction("Index", "DealerLogin");
        }

        [HttpPost]
        public ActionResult Authorize(dealer loginmodel)
        {
            var dealerdetails = model.dealers.Where(x => x.Email == loginmodel.Email && x.Password == loginmodel.Password).FirstOrDefault();
            if (dealerdetails == null)
            {
                TempData["error"] = "Wrong Email and/or Password";
                return RedirectToAction("loginpage","DealerLogin");
            }
            else
            {
                Session["Username"] = dealerdetails.Name;
                Session["Password"] = dealerdetails.Password;
                Session["ID"] = dealerdetails.id;
                return RedirectToAction("Index", "DealerLogin");
            }
        }

        public ActionResult Logout()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            Session.Abandon();
            return RedirectToAction("loginpage", "DealerLogin");
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
                return RedirectToAction("resetpassword","DealerLogin");
            }

            string Password = model.dealers.Where(m => m.Email == email).Select(m => m.Password).First();

            MailMessage mailMessage = new MailMessage("patelpoojan602@gmail.com", email);
            mailMessage.Subject = "Password Recovery";
            mailMessage.Body = "Your Password : " + Password;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);

            return RedirectToAction("loginpage", "DealerLogin");
        }

        public ActionResult Account()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/DealerLogin/loginpage");
            }

            if (TempData["success"] != null)
            {
                ViewBag.successchange = TempData["success"].ToString();
            }
            if (TempData["wrngpass"] != null)
            {
                ViewBag.wrongoldpass = TempData["wrngpass"].ToString();
            }

            long id1 = Convert.ToInt32(Session["ID"]);

            DealerAreamodelSubscriptionselectedDeliveryslotJoin dlrjoin = new DealerAreamodelSubscriptionselectedDeliveryslotJoin();

            dlrjoin = (from dlr in model.dealers.Where(m => m.id == id1)
                       join ct in model.cities on dlr.City equals ct.id
                       join subselected in model.subscriptionselecteds.Where(m => m.dealerid == id1) on dlr.id equals subselected.dealerid
                       join areaaa in (
                           from areaselect in model.areaselecteds.Where(m => m.dealerid == id1)
                           join areas in model.areas on areaselect.areaid equals areas.id
                           select new AreaModel { area = areas, areaselected = areaselect }
                       ) on dlr.id equals areaaa.areaselected.dealerid into areaas
                       select new DealerAreamodelSubscriptionselectedDeliveryslotJoin
                       {
                           objdlr = dlr,
                           objct = ct,
                           objsubselected = subselected,
                           areamodel = areaas.ToList(),
                       }).SingleOrDefault();

            return View(dlrjoin);
        }

        public ActionResult changepass(DealerAreamodelSubscriptionselectedDeliveryslotJoin objmodel)
        {
            long id = Convert.ToInt32(Session["ID"]);
            dealer dlr = model.dealers.Where(m => m.id == id).SingleOrDefault();

            if(dlr.Password == objmodel.objdlr.OldPassword)
            {
                dlr.Password = objmodel.objdlr.NewPassword;
                TempData["success"] = "Password Changed Successfully";
                model.Entry(dlr).State = EntityState.Modified;
                model.SaveChanges();
            }
            else
            {
                TempData["wrngpass"] = "Wrong Old Password";       
            }    
            return RedirectToAction("Account","DealerLogin");
        }
    }

}