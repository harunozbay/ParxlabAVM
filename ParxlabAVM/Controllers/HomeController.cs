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
           /* Model veritabani = new Model();

            veritabani.anatablo.Add(new anatablo
            {
                parkid = 1,
                aracplakasi = "Harun",
                cihazid = 3,
                giriszamani = new DateTime(2018, 07, 19, 13, 00, 0),
                //cikiszamani = new DateTime(2018, 07, 19, 8, 35, 0),
                kullaniciid = "Ankabeta",
                firmaid = 1
            });

            (from veri in veritabani.cihaz where veri.cihazid == 3 select veri).FirstOrDefault().cihazdurumu=1;
            veritabani.SaveChanges();*/

            DateTime simdi = DateTime.Now;
            DateTime dunBuSular = simdi.AddDays(-1);
            DateTime gununBaslangici = DateTime.Today;
            DateTime dununBaslangici = gununBaslangici.AddDays(-1);
            int gecenSaniye = (int)simdi.Subtract(gununBaslangici).TotalSeconds;

            int bugunkuAracSayisi = (int)GrafikVeriOlusturucu.ZamanDilimindeGirenArac(1, gununBaslangici, simdi, gecenSaniye)[0].Deger;
            //dünkü araç sayısı
            double toplamParkSuresi = GrafikVeriOlusturucu.ZamanDilimindeAraclarınHarcadigiToplamZaman(1, gununBaslangici, simdi, gecenSaniye)[0].Deger;
            //dünkü toplamParkSuresi
            double anlikDoluluk = GrafikVeriOlusturucu.AnlikDolulukOrani(1) * 100;
            //dünkü anlikDoluluk

            ViewBag.bugunkuAracSayisi = bugunkuAracSayisi;
            ViewBag.toplamParkSuresi = toplamParkSuresi.ToString("#.##");
            ViewBag.ortalamaParkSuresi = (toplamParkSuresi / bugunkuAracSayisi).ToString("#.##");
            ViewBag.anlikDoluluk = anlikDoluluk.ToString("#.##");
            
            return View();





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

            DateTime girisZamani = DateTime.ParseExact(Giris, "dd/MM/yyyy HH:mm", null);
            DateTime cikisZamani = DateTime.ParseExact(Cikis, "dd/MM/yyyy HH:mm", null);

            List<int> arabaSayisi = new List<int>();
            List<string> etiketler = new List<string>();
            string[] format = { "dd", "MM" };
            foreach (var i in GrafikVeriOlusturucu.GunlereGoreGirenArac(1, girisZamani, cikisZamani))
            {
                arabaSayisi.Add((int)(i.Deger));
                etiketler.Add(GrafikVeriOlusturucu.GrafikVeriEtiketiOlustur(i, format, false));

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
