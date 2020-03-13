using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customer.Models;
using Customer.ViewModel;
using PagedList;

namespace Customer.Controllers
{
    [HandleError(View ="Error")]
    [RoutePrefix("Customers")]
    public class customerController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        // GET: customer
        [Route("List")]
        
        public ActionResult Index(int ? page)
        {
            int pagesize = 3;
            int pagenumber = page ?? 1;
            List<customerViewModel> list = new List<customerViewModel>();
            using (CustomerEntities db=new CustomerEntities())
            {
            
                List<customer> clist = db.customers.ToList();
                foreach(customer c in clist)
                {
                    customerViewModel model = new customerViewModel();
                    model.id = c.id;
                    model.name = c.name;
                    model.address1 = c.address1;
                    model.address2 = c.address2;
                    model.birthdate = c.birthdate;
                    model.country = c.country;
                    model.createddate = DateTime.Now;
                    model.updateddate = DateTime.Now;
                    model.email = c.email;
                    model.postcode = c.postcode;
                    model.mobile = c.mobile;
                    model.active = c.active;
                    model.city = c.City.name;
                    list.Add(model);
                   // throw new Exception();
                }
                
                return View(list.ToPagedList(pagenumber,pagesize));
            }
           
        }

        // GET: customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: customer/Create
        
        public ActionResult Create()
        {
            ViewData["City"] = new SelectList(db.Cities, "cid", "Name"); 
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( customerViewModel customer)
        {
            ViewData["City"] = new SelectList(db.Cities, "cid", "Name");
            using (CustomerEntities db = new CustomerEntities())
            {
                customer model = new customer();
                model.id = customer.id;
                model.name = customer.name;
                model.address1 = customer.address1;
                model.address2 = customer.address2;
                model.birthdate = customer.birthdate;
                model.country = customer.country;
                model.createddate = customer.createddate;
                model.email = customer.email;
                model.createddate = DateTime.Now;
                model.updateddate = DateTime.Now;
                model.postcode = customer.postcode;
                model.mobile = customer.mobile;
                model.active = customer.active;
                model.cid = Convert.ToInt32(customer.cid);
               
                var isEmailExists = db.customers.Any(x => x.email.Equals(customer.email, StringComparison.OrdinalIgnoreCase));
               
                if (isEmailExists)
                {
                    ModelState.AddModelError("Email", "Email Already Exists!!");
                    return View();
                }
                db.customers.Add(model);
                db.SaveChanges();

            }
                return RedirectToAction("Index");
            }

       
        public ActionResult Edit(int ? id)
        {
            List<SelectListItem> cities;
            customerViewModel customer = new customerViewModel();
            using (CustomerEntities db=new CustomerEntities())
            {
                customer c = db.customers.Find(id);
                customer.id = c.id;
                customer.name = c.name;
                customer.address1 = c.address1;
                customer.address2 = c.address2;
                customer.birthdate = c.birthdate;
                customer.country = c.country;
                customer.createddate = c.createddate;
                customer.updateddate = DateTime.Now;
                customer.email = c.email;
                customer.postcode = c.postcode;
                customer.mobile = c.mobile;
                customer.active = c.active;
                customer.city = c.City.name;
                cities = db.Cities.Select(x=>new SelectListItem { Text=x.name,Value=x.cid.ToString()}).ToList();

            }
            ViewBag.cities = cities;
               
            return View(customer);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( customerViewModel customer)
        {
            List<SelectListItem> cities;
            using (CustomerEntities db = new CustomerEntities())
            {
                customer model = db.customers.Find(customer.id);
                model.id = customer.id;
                model.name = customer.name;
                model.address1 = customer.address1;
                model.address2 = customer.address2;
                model.birthdate = customer.birthdate;
                model.country = customer.country;
                model.createddate = customer.createddate;
                model.email = customer.email;
               
                customer.updateddate = DateTime.Now;
                model.postcode = customer.postcode;
                model.mobile = customer.mobile;
                model.active = customer.active;
                model.cid = Convert.ToInt32(customer.city);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                cities = db.Cities.Select(x => new SelectListItem { Text = x.name, Value = x.cid.ToString() }).ToList();
            }
            ViewBag.cities = cities;
            return RedirectToAction("Index");
        }

        // GET: customer/Delete/5
        [HttpGet]
      
        public ActionResult GoToDelete(int? id)
        {
            return View("Delete");
        }

        [HttpGet]
        public JsonResult Delete(int ?id)
        {
            customerViewModel customer = new customerViewModel(); 
            using (CustomerEntities db=new CustomerEntities())
            {
                customer c = db.customers.Find(id);
                customer.id = c.id;
                customer.name = c.name;
                customer.address1 = c.address1;
                customer.address2 = c.address2;
                customer.birthdate = c.birthdate;
                customer.country = c.country;
                customer.createddate = c.createddate;
                customer.updateddate = c.updateddate;
                customer.email = c.email;
                customer.postcode = c.postcode;
                customer.mobile = c.mobile;
                customer.active = c.active;
                customer.city = c.City.name;

            }
                return Json(customer,JsonRequestBehavior.AllowGet);
        }

        // POST: customer/Delete/5
        [HttpPost]
       
        public ActionResult DeleteConfirmed(int id)
        {
            customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index","customer");
        }
        public ActionResult NotFound()
        {
            return View();
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
