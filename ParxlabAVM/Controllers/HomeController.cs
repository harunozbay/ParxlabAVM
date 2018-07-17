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
            string[] kullanicilar = { "harun","cihat","önder","burak","özlem","onur","berk","halil","hüseyin" };
            string[] plakalar = { "14 ASD 06" , "06 IST 34" , "34 DSA 34" , "34 PLT 34" , "05 GS 05" , "10 ON 10",
                                  "61 LAZ 61" ,"41 ZXC 14"  ,  "01 ADN 100"  };
            for (int i = 0; i < 20; i++)
            {

                int gun = rnd.Next(12, 29);
                int saat = rnd.Next(0, 10);
                int saat2 = rnd.Next(saat, 24);
                int cid = rnd.Next(1, 6);//cihaz id
                int pid = rnd.Next(1, 20);//park id
                int fid = rnd.Next(1, 2);//firma id
                int kisi = rnd.Next(0, 9);//kisi no
                veritabani.anatablo.Add(new anatablo
                {
                    
                    parkid = 1,
                    aracplakasi = plakalar[kisi],
                    cihazid = 1,
                    giriszamani = new DateTime(2018, 06, gun, saat, 0, 0),
                    cikiszamani = new DateTime(2018, 06, gun, saat2, 0, 0),
                    kullaniciid = "Ankabeta",
                    firmaid = 1
                });

                
            }

            veritabani.SaveChanges();*/

           

            var anatablo = db.anatablo.Include(a => a.cihaz).Include(a => a.firma).Include(a => a.kullanici).Include(a => a.parkyeri).OrderBy(a => a.giriszamani);
            return View(anatablo.ToList());
        }

        //grafik sayfası getirici
        [HttpGet]
        public ActionResult GrafikForm()
        {
            return View();
        }

        //grafik veri gönderici
        [HttpPost]
        public ActionResult GrafikForm(string Giris, string Cikis)
        {
            
            DateTime girisZamani = DateTime.ParseExact(Giris, "dd/MM/yyyy HH:mm",null );
            DateTime cikisZamani = DateTime.ParseExact(Cikis, "dd/MM/yyyy HH:mm", null);

            List<int> arabaSayisi = new List<int>();
            List<string> etiketler = new List<string>();
            string[] format = { "dd", "MM" };
            foreach (var i in GrafikVeriOlusturucu.GunlereGoreGirenArac(1, girisZamani, cikisZamani))
            {
                arabaSayisi.Add((int)(i.Deger));
                etiketler.Add(GrafikVeriOlusturucu.GrafikVeriEtiketiOlustur(i,format,false));

            }

            ViewBag.Etiketler = etiketler;
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
