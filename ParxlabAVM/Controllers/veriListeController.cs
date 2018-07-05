using ParxlabAVM.Helpers;
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
            return View(olusturucu.HaftalaraGöreGirenArac(1,new DateTime(2018,06,25,0,0,0),new DateTime(2018,07,05,23,59,0)));
        }
    }
}