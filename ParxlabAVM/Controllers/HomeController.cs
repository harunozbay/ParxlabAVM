using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParxlabAVM.Models;
using ParxlabAVM.Helpers;
using System.Globalization;

namespace ParxlabAVM.Controllers
{
    public class HomeController : Controller
    {
        private Model db = new Model();

        // GET: Home
        public ActionResult Index()
        {
            /*Random rnd = new Random();
            Model veritabani = new Model();
            for (int i = 0; i < 11; i++)
            {

                int gun = rnd.Next(18, 24);
                int saat = rnd.Next(0, 20);
                int saat2 = rnd.Next(saat, 24);
                veritabani.anatablo.Add(new anatablo
                {
                    
                    parkid = 1,
                    aracplakasi = "06 ASD 34",
                    cihazid = 1,
                    giriszamani = new DateTime(2018, 06, 24, saat, 15, 0),
                    cikiszamani = new DateTime(2018, 06, 24, saat2, 35, 0),
                    kullaniciid = "Ankabeta",
                    firmaid = 1
                });

                
            }

            veritabani.SaveChanges();*/

           

            var anatablo = db.anatablo.Include(a => a.cihaz).Include(a => a.firma).Include(a => a.kullanici).Include(a => a.parkyeri).OrderBy(a => a.giriszamani);
            return View(anatablo.ToList());
        }

        //sayfa getirici
        [HttpGet]
        public ActionResult GrafikForm()
        {
            return View();
        }

        //veri gönderici
        [HttpPost]
        public ActionResult GrafikForm(string Giris, string Cikis)
        {
            GrafikVeriOlusturucu gvo = new GrafikVeriOlusturucu();
            DateTime girisZamani = DateTime.ParseExact(Giris, "dd/MM/yyyy HH:mm",null );
            DateTime cikisZamani = DateTime.ParseExact(Cikis, "dd/MM/yyyy HH:mm", null);

            List<double> arabaSayisi = new List<double>();
            foreach (var i in gvo.GünlereGöreGirenArac(1, girisZamani, cikisZamani))
            {
                arabaSayisi.Add(i.Deger);

            }

            return View("GrafikCiz", arabaSayisi);
        }

        // GET: Home/Details/5
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

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.cihazid = new SelectList(db.cihaz, "cihazid", "cihazid");
            ViewBag.firmaid = new SelectList(db.firma, "id", "firmaadi");
            ViewBag.kullaniciid = new SelectList(db.kullanici, "kullaniciid", "sifre");
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "anatabloid,cihazid,parkid,firmaid,giriszamani,cikiszamani,aracplakasi,kullaniciid")] anatablo anatablo)
        {
            if (ModelState.IsValid)
            {
                db.anatablo.Add(anatablo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cihazid = new SelectList(db.cihaz, "cihazid", "cihazid", anatablo.cihazid);
            ViewBag.firmaid = new SelectList(db.firma, "id", "firmaadi", anatablo.firmaid);
            ViewBag.kullaniciid = new SelectList(db.kullanici, "kullaniciid", "sifre", anatablo.kullaniciid);
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", anatablo.parkid);
            return View(anatablo);
        }

        // GET: Home/Edit/5
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
            ViewBag.firmaid = new SelectList(db.firma, "id", "firmaadi", anatablo.firmaid);
            ViewBag.kullaniciid = new SelectList(db.kullanici, "kullaniciid", "sifre", anatablo.kullaniciid);
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", anatablo.parkid);
            return View(anatablo);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "anatabloid,cihazid,parkid,firmaid,giriszamani,cikiszamani,aracplakasi,kullaniciid")] anatablo anatablo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anatablo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cihazid = new SelectList(db.cihaz, "cihazid", "cihazid", anatablo.cihazid);
            ViewBag.firmaid = new SelectList(db.firma, "id", "firmaadi", anatablo.firmaid);
            ViewBag.kullaniciid = new SelectList(db.kullanici, "kullaniciid", "sifre", anatablo.kullaniciid);
            ViewBag.parkid = new SelectList(db.parkyeri, "parkid", "parkadi", anatablo.parkid);
            return View(anatablo);
        }

        // GET: Home/Delete/5
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

        // POST: Home/Delete/5
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
