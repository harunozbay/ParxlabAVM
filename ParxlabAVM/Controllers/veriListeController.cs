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

            GrafikVeriOlusturucu olusturucu = new GrafikVeriOlusturucu();
            List<string> kisaltmalar = new List<string>();
            List<ZamanAraligiVerisi> veriler = olusturucu.GunlereGoreAraclarınHarcadigiToplamZaman(1, new DateTime(2018, 06, 25, 0, 0, 0), new DateTime(2018, 07, 05, 23, 59, 0));
            foreach (var item in veriler)
            {
                kisaltmalar.Add(olusturucu.GrafikVeriEtiketiOlustur(item, 1, 0, true));
            }
            return View(kisaltmalar);
        }
        public ActionResult Anatablo()
        {
            Model vt = new Model();
            GrafikVeriOlusturucu olusturucu = new GrafikVeriOlusturucu();
            return View((from veri in vt.anatablo orderby veri.giriszamani select veri).ToList());
        }
    }
}