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
    public class anatabloController : Controller
    {
        private Model db = new Model();

        // GET: anatablo
        public ActionResult Index()
        {
            var anatablo = db.anatablo.Include(a => a.cihaz).Include(a => a.firma).Include(a => a.kullanici).Include(a => a.parkyeri);
            return View(anatablo.ToList());
        }

        // GET: anatablo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anatablo anatablo = db.anatablo.Find(id);
            if (anatablo == null)
            {
                return HttpNotFound();
            }
            return View(anatablo);
        }

        // GET: anatablo/Create
        public ActionResult Create()
        {
            ViewBag.cihazid = new SelectList(db.cihaz, "cihazid", "cihazid");
            ViewBag.firmaid = new SelectList(db.firma, "firmaid", "firmaadi");
            ViewBag.kullaniciid = new SelectList(db.kullanici, "kullaniciid", "sifre");
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi");
            return View();
        }

        // POST: anatablo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "anatabloid,firmaid,parkid,cihazid,giriszamani,cikiszamani,aracplakasi,kullaniciid")] anatablo anatablo)
        {
            if (ModelState.IsValid)
            {
                db.anatablo.Add(anatablo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cihazid = new SelectList(db.cihaz, "cihazid", "cihazid", anatablo.cihazid);
            ViewBag.firmaid = new SelectList(db.firma, "firmaid", "firmaadi", anatablo.firmaid);
            ViewBag.kullaniciid = new SelectList(db.kullanici, "kullaniciid", "sifre", anatablo.kullaniciid);
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", anatablo.parkid);
            return View(anatablo);
        }

        // GET: anatablo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anatablo anatablo = db.anatablo.Find(id);
            if (anatablo == null)
            {
                return HttpNotFound();
            }
            ViewBag.cihazid = new SelectList(db.cihaz, "cihazid", "cihazid", anatablo.cihazid);
            ViewBag.firmaid = new SelectList(db.firma, "firmaid", "firmaadi", anatablo.firmaid);
            ViewBag.kullaniciid = new SelectList(db.kullanici, "kullaniciid", "sifre", anatablo.kullaniciid);
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", anatablo.parkid);
            return View(anatablo);
        }

        // POST: anatablo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "anatabloid,firmaid,parkid,cihazid,giriszamani,cikiszamani,aracplakasi,kullaniciid")] anatablo anatablo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anatablo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cihazid = new SelectList(db.cihaz, "cihazid", "cihazid", anatablo.cihazid);
            ViewBag.firmaid = new SelectList(db.firma, "firmaid", "firmaadi", anatablo.firmaid);
            ViewBag.kullaniciid = new SelectList(db.kullanici, "kullaniciid", "sifre", anatablo.kullaniciid);
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", anatablo.parkid);
            return View(anatablo);
        }

        // GET: anatablo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anatablo anatablo = db.anatablo.Find(id);
            if (anatablo == null)
            {
                return HttpNotFound();
            }
            return View(anatablo);
        }

        // POST: anatablo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            anatablo anatablo = db.anatablo.Find(id);
            db.anatablo.Remove(anatablo);
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
