using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Customer.Models;
using Customer.ViewModel;

namespace Customer.Areas.CityArea.Controllers
{
    [HandleError(View ="Error")]
    //[RouteArea("CityArea",AreaPrefix = "CityAreas")]
    //[RoutePrefix("Cities")]
    
    public class CityController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        // GET: CityArea/City
        [OutputCache(Duration = 5, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        //[Route("List")]
        public ActionResult Index()
        {
            List<cityViewModel> list = new List<cityViewModel>();
            using (CustomerEntities db = new CustomerEntities())
            {

                List<City> clist = db.Cities.ToList();
                foreach (City c in clist)
                {
                    cityViewModel model = new cityViewModel();
                    model.cid = c.cid;
                    model.name = c.name;
                    model.CreatedDate = c.CreatedDate;
                    model.UpdatedDate = c.UpdatedDate;
                    list.Add(model);
                    //throw new Exception();
                }
                return View(list);
            }

        }
    

        // GET: CityArea/City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: CityArea/City/Create
      
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cityViewModel city)
        {
            using (CustomerEntities db = new CustomerEntities())
            {
                City model = new City();
                model.cid = city.cid;
                model.name = city.name;
                model.CreatedDate = city.CreatedDate;
                model.UpdatedDate = city.UpdatedDate;
                db.Cities.Add(model);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
           
        }

        // GET: CityArea/City/Edit/5
        
        public ActionResult Edit(int? id)
        {
            cityViewModel city = new cityViewModel();
            using (CustomerEntities db = new CustomerEntities())
            {
                City c = db.Cities.Find(id);
                city.cid = c.cid;
                city.name = c.name;
                city.CreatedDate = c.CreatedDate;
                city.UpdatedDate = c.UpdatedDate;
            }
            return View(city);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cityViewModel city)
        {

            using (CustomerEntities db = new CustomerEntities())
            {
                City model = db.Cities.Find(city.cid);
                model.cid = city.cid;
                model.name = city.name;
                model.CreatedDate = city.CreatedDate;
                model.UpdatedDate = city.UpdatedDate;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                
            }
           
            return RedirectToAction("Index");


        }


        // GET: CityArea/City/Delete/5
      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: CityArea/City/Delete/5
        [HttpPost, ActionName("Delete")]
     
        public ActionResult DeleteConfirmed(int id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
