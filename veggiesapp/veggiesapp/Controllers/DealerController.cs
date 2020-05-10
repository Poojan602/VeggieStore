using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using veggiesapp.Models;
using System.Data.Entity;

namespace veggiesapp.Controllers
{
    public class DealerController : Controller
    {
        sabziappEntities model = new sabziappEntities();

        public ActionResult activedealer()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            IEnumerable<DealerCityJoin> dealercity;

            dealercity = (from dlr in model.dealers.Where(m => m.Status == true)
                          join ct in model.cities on dlr.City equals ct.id
                          select new DealerCityJoin
                          {
                              objcity = ct,
                              objdlr = dlr
                          }).OrderByDescending(m => m.objdlr.Addeddate).ToList();

            return View(dealercity);

        }

        public ActionResult suspendeddealer()
        {
            if (Session["Username"] == null && Session["Password"] == null)
            {
                Response.Redirect("~/Login/Index");
            }

            IEnumerable<DealerCityJoin> dealercity;

            dealercity = (from dlr in model.dealers.Where(m => m.Status == false)
                          join ct in model.cities on dlr.City equals ct.id
                          select new DealerCityJoin
                          {
                              objcity = ct,
                              objdlr = dlr
                          }).OrderByDescending(m => m.objdlr.Addeddate).ToList();

            return View(dealercity);
        }

        public ActionResult activetosuspend(string id)
        {
            long id1 = (long)Convert.ToDouble(id);

            dealer objdealer = model.dealers.Where(s => s.id == id1).SingleOrDefault();
            if (objdealer != null)
            {
                objdealer.Status = false;
                model.Entry(objdealer).State = EntityState.Modified;
                model.SaveChanges();
            }

            return RedirectToAction("activedealer");
        }

        public ActionResult suspendtoactive(string id)
        {
            long id1 = (long)Convert.ToDouble(id);

            var dlr = new dealer()
            {
                id = id1,
                Status = true
            };

            model.dealers.Attach(dlr);
            model.Entry(dlr).Property(x => x.Status).IsModified = true;
            model.SaveChanges();

            return RedirectToAction("suspendeddealer");
        }

        public ActionResult viewdealerdetails(string id)
        {
            long id1 = (long)Convert.ToDouble(id);

            List<deliveryslot> ds = model.deliveryslots.Where(m => m.Dealerid == id1).ToList();
            deliveryslot objdelslot = new deliveryslot();

            if (ds.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    objdelslot.Dealerid = id1;
                    model.Entry(objdelslot).State = EntityState.Added;
                    model.SaveChanges();
                }
            }

            DealerAreamodelSubscriptionselectedDeliveryslotJoin dlrjoin = new DealerAreamodelSubscriptionselectedDeliveryslotJoin();

            dlrjoin = (from dlr in model.dealers.Where(m => m.id == id1)
                       join ct in model.cities on dlr.City equals ct.id
                       join delislot in model.deliveryslots.Where(m => m.Dealerid == id1) on dlr.id equals delislot.Dealerid into deliveryslots
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
                           objdeliveryslot = deliveryslots.ToList(),
                           areamodel = areaas.ToList(),
                       }).SingleOrDefault();

            return View(dlrjoin);
        }

        public ActionResult Addareas(long id)
        {
            long dealerid = id;
            long? cityid = model.dealers.Where(m => m.id == dealerid).Select(m => m.City).SingleOrDefault();

            TempData["DLRID"] = dealerid;
            TempData["Cityname"] = model.cities.Where(m => m.id == cityid).Select(m => m.Name).SingleOrDefault();
            
            List<areaselected> list = list = model.areaselecteds.Where(m => m.dealerid == dealerid || m.dealerid == null).ToList();
            
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].dealerid == null)
                {
                    list[i].ischecked = false;
                }
                else
                {
                    list[i].ischecked = true;
                }
            }

            List<SelectListItem> items = new List<SelectListItem>();
            List<AreaModel> areaareaselected;

            areaareaselected = (from area in model.areas.Where(m => m.cityid == cityid)
                                join arselected in model.areaselecteds.Where(m => m.dealerid == null || m.dealerid == dealerid) on area.id equals arselected.areaid
                                select new AreaModel
                                {
                                    area = area,
                                    areaselected = arselected
                                }).ToList();
            
            return View(areaareaselected);
        }

        [HttpPost]
        public ActionResult UpdateArea(List<AreaModel> AreaModel)
        {
            long dlrid = Convert.ToInt32(TempData["DealerID"]);

            foreach (var item in AreaModel)
            {
                if (item.areaselected.ischecked == false)
                {
                    item.areaselected.dealerid = null;
                }
                else
                {
                    item.areaselected.dealerid = dlrid;
                }
                model.Entry(item.areaselected).State = EntityState.Modified;
                model.SaveChanges();
            }
            
            return RedirectToAction("viewdealerdetails", "Dealer", new { id = dlrid.ToString() });
        }

        public ActionResult adddeliveryslots(DealerAreamodelSubscriptionselectedDeliveryslotJoin join)
        {
            foreach (var item in join.objdeliveryslot)
            {
                model.Entry(item).State = EntityState.Modified;
                model.SaveChanges();
            }
            return RedirectToAction("activedealer", "Dealer");
        }

        public ActionResult updateareaselected(DealerAreamodelSubscriptionselectedDeliveryslotJoin join)
        {
            return RedirectToAction("activedealer", "Dealer");
        }
    }
}