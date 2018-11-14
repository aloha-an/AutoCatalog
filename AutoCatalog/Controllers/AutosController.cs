using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoCatalog.Models;

namespace AutoCatalog.Controllers
{
   
    public class AutosController : Controller
    {
        private AutoEntities db = new AutoEntities();
               
        // GET: Autos
        public ActionResult Index()
        {
            int ID = Convert.ToInt32(Session["id"]);
            TempData["UserID"] = ID;
           
            var autos = db.Autos.Include(a => a.Colors).Include(a => a.Makes).Include(a => a.CarModels).Include(a => a.Users).Where(a=>a.UserID==ID).
                OrderBy(a=>a.RegistryNum).OrderBy(a=>a.Users.Email).OrderBy(a=>a.Makes.Make).OrderBy(a=>a.CarModels.CarModel);
            if (Session["role"].ToString()=="admin")
            {
                return View(autos.ToList());
            }
            else
            {
                return View("UserIndex", autos.ToList());
            }            
        }

        public ActionResult AllCars()
        {
            var autos = db.Autos.Include(a => a.Colors).Include(a => a.Makes).Include(a => a.CarModels).Include(a => a.Users).
                OrderBy(a => a.RegistryNum).OrderBy(a => a.Users.Email).OrderBy(a => a.Makes.Make).OrderBy(a => a.CarModels.CarModel);
            if (Session["role"].ToString() == "admin")
            {
                return View(autos.ToList());
            }
            else
            {
                return View("UserAllCars", autos.ToList());
            }
        }

        [HttpPost]
        public ActionResult SearchCars(string name)
        {
            var cars = db.Autos.Include(a => a.Colors).Include(a => a.Makes).Include(a => a.CarModels).Include(a => a.Users).
                OrderBy(a => a.RegistryNum).OrderBy(a => a.Users.Email).OrderBy(a => a.Makes.Make).OrderBy(a => a.CarModels.CarModel);
            if (String.IsNullOrEmpty(name))
            {
                if (Session["role"].ToString() == "admin")
                {
                    return View(cars);
                }
                else
                {
                    return View("UserSearchCars", cars);
                }
            }
                if (!String.IsNullOrEmpty(name))
            {
                var search = (from t in cars
                              where(t.CarModels.CarModel.Contains(name)||t.Makes.Make.Contains(name)||
                                    t.Users.Email.Contains(name)||t.Colors.Color.Contains(name)||
                                    t.RegistryNum.Contains(name))
                              select t);
                if (search.Count() <= 0 )
                    return HttpNotFound();

                if (Session["role"].ToString() == "admin")
                {
                    return View(search);
                }
                else
                {
                    return View("UserSearchCars", search);
                }
            }
            else
            {                
                return PartialView();
            }
            
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autos autos = db.Autos.Find(id);
            if (autos == null)
            {
                return HttpNotFound();
            }
            
            if (Session["role"].ToString() == "admin")
            {
                return View(autos);
            }
            else
            {
                return View("UserDetails", autos);
            }
        }

        // GET: Autos/Create     
        public ActionResult Create()
        {
            int selectedIndex = 1;
            int ID = Convert.ToInt32(Session["id"]);
            TempData["UserID"] = ID;
            Users one = new Users();
            one = db.Users.Find(ID);
            SelectList users = new SelectList(db.Users, "ID", "Email", ID);
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Color");
            SelectList makes= new SelectList(db.Makes, "ID", "Make",selectedIndex);
            ViewBag.MakeID = makes;
            SelectList carModels = new SelectList(db.CarModels.Where(c => c.MakeID == selectedIndex), "ID", "CarModel");
            ViewBag.ModeLID = carModels;
            ViewBag.UserID = users;
            
            if (Session["role"].ToString() == "admin")
            {
                return View();
            }
            else
            {
                return View("UserCreate");
            }
        }

        // POST: Autos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MakeID,ModeLID,ColorID,RegistryNum")] Autos autos)
        {
            //ViewData["usID"] = ID;
            autos.UserID = Convert.ToInt32(Session["id"]);
            if (ModelState.IsValid)
            {
                db.Autos.Add(autos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Color", autos.ColorID);
            ViewBag.MakeID = new SelectList(db.Makes, "ID", "Make", autos.MakeID);
            ViewBag.ModeLID = new SelectList(db.CarModels, "ID", "CarModel", autos.ModeLID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", autos.UserID);

            if (Session["role"].ToString() == "admin")
            {
                return View(autos);
            }
            else
            {
                return View("UserCreate", autos);
            }            
        }

        // GET: Autos/Edit/5
        public ActionResult Edit(int? id)
        {
            int selectedIndex = 1;
            int ID = Convert.ToInt32(Session["id"]);
            TempData["UserID"] = ID;
            Users one = new Users();
            one = db.Users.Find(ID);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autos autos = db.Autos.Find(id);
            if (autos == null)
            {
                return HttpNotFound();
            }
                
            SelectList users = new SelectList(db.Users, "ID", "Email", ID);
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Color");
            SelectList makes = new SelectList(db.Makes, "ID", "Make", selectedIndex);
            ViewBag.MakeID = makes;
            SelectList carModels = new SelectList(db.CarModels.Where(c => c.MakeID == selectedIndex), "ID", "CarModel");
            ViewBag.ModeLID = carModels;
            ViewBag.UserID = users;
            ViewBag.Email = one.Email;

            if (Session["role"].ToString() == "admin")
            {
                return View(autos);
            }
            else
            {
                return View("UserEdit", autos);
            }
        }

        // POST: Autos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,MakeID,ModeLID,ColorID,RegistryNum")] Autos autos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Color", autos.ColorID);
            ViewBag.MakeID = new SelectList(db.Makes, "ID", "Make", autos.MakeID);
            ViewBag.ModeLID = new SelectList(db.CarModels, "ID", "CarModel", autos.ModeLID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", autos.UserID);

            if (Session["role"].ToString() == "admin")
            {
                return View(autos);
            }
            else
            {
                return View("UserEdit", autos);
            }
        }

        // GET: Autos/Delete/5
        public ActionResult Delete(int? id)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autos autos = db.Autos.Find(id);
            if (autos == null)
            {
                return HttpNotFound();
            }

            if (Session["role"].ToString() == "admin")
            {
                return View(autos);
            }
            else
            {
                return View("UserDelete", autos);
            }
        }

        // POST: Autos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autos autos = db.Autos.Find(id);
            db.Autos.Remove(autos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCarModel(int ID)
        {
            return PartialView(db.CarModels.Where(c=>c.MakeID==ID).ToList());
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
