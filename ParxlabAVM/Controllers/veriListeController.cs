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

            /*List<string> kisaltmalar = new List<string>();
            List<ZamanAraligiVerisi> veriler = GrafikVeriOlusturucu.GunlereGoreGirenArac(1, new DateTime(2018, 06, 25, 0, 0, 0), new DateTime(2018, 07, 05, 23, 59, 0));
            foreach (var item in veriler)
            {
                kisaltmalar.Add(GrafikVeriOlusturucu.GrafikVeriEtiketiOlustur(item, true,"ddd", "tr-TR"));
            }*/
            return View(GrafikVeriOlusturucu.AylaraGoreGirenArac(1, new DateTime(2018, 05, 31, 0, 30, 0), new DateTime(2018, 12, 31, 0, 30, 0)));
        }
        public ActionResult Anatablo()
        {
            Model vt = new Model();
            return View((from veri in vt.anatablo orderby veri.giriszamani select veri).ToList());
        }
    }
}