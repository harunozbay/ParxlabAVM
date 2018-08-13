using ParxlabAVM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParxlabAVM.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult GirenArabaSayisiGrafigi()
        {
            return View();
        }
        

        //grafik veri gönderici
        [HttpPost]
        public ActionResult GirenArabaSayisiGrafigi(string Giris, string Cikis)
        {

            DateTime girisZamani = DateTime.ParseExact(Giris, "dd/MM/yyyy HH:mm", null);
            DateTime cikisZamani = DateTime.ParseExact(Cikis, "dd/MM/yyyy HH:mm", null);

            List<int> arabaSayisi = new List<int>();
            List<string> etiketler = new List<string>();
            string format =  "dd/MM" ;
            foreach (var i in GrafikVeriOlusturucu.GunlereGoreGirenArac(1, 'f',girisZamani, cikisZamani))
            {
                arabaSayisi.Add((int)(i.Deger));
                etiketler.Add(GrafikVeriOlusturucu.GrafikVeriEtiketiOlustur(i, format, false));

            }

            ViewBag.Etiketler = etiketler;
            return View("GrafikCiz", arabaSayisi);
        }
    }
}