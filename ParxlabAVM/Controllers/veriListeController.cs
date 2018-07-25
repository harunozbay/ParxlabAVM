using ParxlabAVM.Helpers;
using ParxlabAVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParxlabAVM.Controllers
{
    public class veriListeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(GrafikVeriOlusturucu.AylaraGoreGirenArac(24, 'c', new DateTime(2016, 12, 01, 0, 30, 0), new DateTime(2018, 12, 31, 0, 30, 0)));
        }

        public ActionResult Double(int id)
        {
            return View(new ZamanAraligiVerisi {
                Baslangic = new DateTime(2018, 7, 19, 0, 0, 0),
                Bitis = new DateTime(2018, 7, 19, 20, 10, 0),
                Deger = GrafikVeriOlusturucu.listedekilerinHarcadigiToplamZaman
                (GrafikVeriOlusturucu.dilimdeBulunmusAraclar(id, 'p', new DateTime(2018, 7, 19, 0, 0, 0), new DateTime(2018, 7, 19, 20, 10, 0))
                , new DateTime(2018, 7, 19, 0, 0, 0), new DateTime(2018, 7, 19, 20, 10, 0))
            });
        }
        public ActionResult Aylik(int id)
        {

            return View(GrafikVeriOlusturucu.AylaraGoreGirenArac(id, 'p', new DateTime(2018, 1, 01, 0, 0, 0), new DateTime(2018, 6, 30, 23, 59, 59)));
        }
        public ActionResult AnlikDoluluk(int id)
        {

            return View(GrafikVeriOlusturucu.HerhangiBirAndaDolulukOranı(id,'p', new DateTime(2018, 7, 19, 11, 0, 0)));
        }
        public ActionResult Gunluk(int id)
        {

            return View(GrafikVeriOlusturucu.GunlereGoreGirenArac(id, 'p', new DateTime(2018, 4, 01, 0, 0, 0), new DateTime(2018, 6, 30, 23, 59, 59)));
        }
        public ActionResult Haftalik(int id)
        {
            return View(GrafikVeriOlusturucu.HaftalaraGoreGirenArac(id, 'p', new DateTime(2018, 1, 01, 0, 0, 0), new DateTime(2018, 8, 5, 23, 59, 0)));
        }
        public ActionResult Yillik(int id)
        {
            return View(GrafikVeriOlusturucu.YillaraGoreGirenArac(id, 'p', new DateTime(2016, 1, 01, 0, 0, 0), new DateTime(2018, 12, 31, 23, 59, 0)));
        }
        public ActionResult Saatlik(int id)
        {
            return View(GrafikVeriOlusturucu.SaatlereGoreGirenArac(id, 'p', new DateTime(2018, 7, 01, 0, 0, 0), new DateTime(2018, 7, 5, 23, 59, 0)));
        }
        public ActionResult HaftalikOrtalama(int id)
        {
            return View(GrafikVeriOlusturucu.OrtalamaBul(7, GrafikVeriOlusturucu.HaftalaraGoreGirenArac(id, 'p', new DateTime(2018, 1, 01, 0, 0, 0), new DateTime(2018, 8, 5, 23, 59, 0))));
        }
        public ActionResult SaatlikOrtalama(int id)
        {
            return View(GrafikVeriOlusturucu.OrtalamaBul(24, GrafikVeriOlusturucu.SaatlereGoreGirenArac(id, 'p', new DateTime(2018, 1, 01, 0, 0, 0), new DateTime(2018, 8, 5, 23, 59, 0))));
        }
        public ActionResult AylikOrtalama(int id)
        {
            return View(GrafikVeriOlusturucu.AyIcindeOrtalamaBul( GrafikVeriOlusturucu.GunlereGoreGirenArac(id, 'p', new DateTime(2018, 4, 01, 0, 0, 0), new DateTime(2018, 6, 30, 23, 59, 59))));
        }

        public ActionResult Anatablo()
        {
            Model vt = new Model();
            return View((from veri in vt.anatablo orderby veri.parkid orderby veri.giriszamani select veri).ToList());
        }
        public ActionResult AnatabloCihaz(int id)
        {
            Model vt = new Model();
            return View((from veri in vt.anatablo where veri.cihazid==id orderby veri.giriszamani select veri).ToList());
        }
        public ActionResult AnatabloPark(int id)
        {
            Model vt = new Model();
            return View((from veri in vt.anatablo where veri.parkid == id orderby veri.giriszamani select veri).ToList());
        }
    }
}