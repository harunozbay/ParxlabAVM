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
            return View(olusturucu.GunlereGoreAraclarınHarcadigiToplamZaman(1, new DateTime(2018, 06, 25, 0, 0, 0), new DateTime(2018, 06, 28, 23, 59, 0)));
        }
        public ActionResult Anatablo()
        {
            Model vt = new Model();
            GrafikVeriOlusturucu olusturucu = new GrafikVeriOlusturucu();
            return View((from veri in vt.anatablo orderby veri.giriszamani select veri).ToList());
        }
    }
}