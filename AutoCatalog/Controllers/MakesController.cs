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
    public class MakesController : Controller
    {
        private AutoEntities db = new AutoEntities();

        // GET: Makes
        public ActionResult Index()
        {
            return View(db.Makes.ToList());
        }

        // GET: Makes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makes makes = db.Makes.Find(id);
            if (makes == null)
            {                
                return HttpNotFound();
            }
            var models = db.CarModels.Where(c => c.MakeID == makes.ID);
            ViewData["Make"] = makes.Make;
            return View(models.ToList());
        }
       

        // GET: Makes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Makes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Make")] Makes makes)
        {
            if (ModelState.IsValid)
            {
                db.Makes.Add(makes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(makes);
        }

        // GET: Makes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makes makes = db.Makes.Find(id);
            if (makes == null)
            {
                return HttpNotFound();
            }
            return View(makes);
        }

        // POST: Makes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Make")] Makes makes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(makes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(makes);
        }

        // GET: Makes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makes makes = db.Makes.Find(id);
            if (makes == null)
            {
                return HttpNotFound();
            }
            return View(makes);
        }

        // POST: Makes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Makes makes = db.Makes.Find(id);
            db.Makes.Remove(makes);
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
