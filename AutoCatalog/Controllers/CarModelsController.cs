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
    public class CarModelsController : Controller
    {
        private AutoEntities db = new AutoEntities();
        Makes makeOne = new Makes();
        // GET: CarModels
        

        public ActionResult Index()
        {
            var carModels = db.CarModels.Include(c => c.Makes);
            return View(carModels.ToList());
        }

        // GET: CarModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            return View(carModels);
        }

        // GET: CarModels/Create
        public ActionResult Create()
        {
            ViewBag.MakeID = new SelectList(db.Makes, "ID", "Make");            
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MakeID,CarModel")] CarModels carModels)
        {
            if (ModelState.IsValid)
            {                
                db.CarModels.Add(carModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MakeID = new SelectList(db.Makes, "ID", "Make", carModels.MakeID);
            return View(carModels);
        }

        // GET: CarModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }            
            ViewBag.MakeID = new SelectList(db.Makes, "ID", "Make", carModels.MakeID);
            return View(carModels);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MakeID,CarModel")] CarModels carModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakeID = new SelectList(db.Makes, "ID", "Make", carModels.MakeID);
            return View(carModels);
        }

        // GET: CarModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            return View(carModels);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarModels carModels = db.CarModels.Find(id);
            db.CarModels.Remove(carModels);
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
