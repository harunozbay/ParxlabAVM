using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParxlabAVM.Models;

namespace ParxlabAVM.Controllers
{
    public class cihazController : Controller
    {
        private Model db = new Model();

        // GET: cihaz
        public ActionResult Index()
        {
            var cihaz = db.cihaz.Include(c => c.parkyeri);
            return View(cihaz.ToList());
        }

        // GET: cihaz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cihaz cihaz = db.cihaz.Find(id);
            if (cihaz == null)
            {
                return HttpNotFound();
            }
            return View(cihaz);
        }

        // GET: cihaz/Create
        public ActionResult Create()
        {
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi");
            return View();
        }

        // POST: cihaz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cihazid,cihazdurumu,enlem,boylam,parkid")] cihaz cihaz)
        {
            if (ModelState.IsValid)
            {
                db.cihaz.Add(cihaz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", cihaz.parkid);
            return View(cihaz);
        }

        // GET: cihaz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cihaz cihaz = db.cihaz.Find(id);
            if (cihaz == null)
            {
                return HttpNotFound();
            }
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", cihaz.parkid);
            return View(cihaz);
        }

        // POST: cihaz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cihazid,cihazdurumu,enlem,boylam,parkid")] cihaz cihaz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cihaz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", cihaz.parkid);
            return View(cihaz);
        }

        // GET: cihaz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cihaz cihaz = db.cihaz.Find(id);
            if (cihaz == null)
            {
                return HttpNotFound();
            }
            return View(cihaz);
        }

        // POST: cihaz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cihaz cihaz = db.cihaz.Find(id);
            db.cihaz.Remove(cihaz);
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
