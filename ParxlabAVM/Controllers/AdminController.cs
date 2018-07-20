using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ParxlabAVM.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParxlabAVM.Controllers
{
   

    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public AdminController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
        }

        // GET: Admin - Yönetici işlemlerinin yapılcağı controller(kullanıcı görüntüle vb.)
        public ActionResult Index()
        {
            return View(userManager.Users);
        }
    }
}