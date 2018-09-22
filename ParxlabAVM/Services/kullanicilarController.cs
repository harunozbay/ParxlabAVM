using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ParxlabAVM.Models;
using ParxlabAVM.Helpers;
using Microsoft.AspNet.Identity;
using ParxlabAVM.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ParxlabAVM.Services
{
    [RoutePrefix("api/kullanicilar")]
    public class kullanicilarController : ApiController
    {
        private Model db = new Model();
        private UserManager<ApplicationUser> userManager;


        public kullanicilarController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));

            userManager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = true,
                RequiredLength = 6,

            };

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false
            };
        }

        public IQueryable<kullanici> Getkullanici()
        {
            return db.kullanici;
        }



        // POST: api/kullanicilar
        [Route("KullaniciDogrula")]
        [ResponseType(typeof(kullanici))]
        [HttpPost]
        public IHttpActionResult KullaniciDogrula(GirisKalibi verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bulunan = userManager.Find(verilen.kullaniciadi, verilen.sifre);

            if (bulunan == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("SifreDegistir")]
        [ResponseType(typeof(kullanici))]
        [HttpPost]
        public IHttpActionResult SifreDegistir(SifreDegistirmeKalibi verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bulunan = userManager.Find(verilen.kullaniciadi, verilen.eskiSifre);

            if (bulunan == null)
            {
                return NotFound();
            }
            bulunan.PasswordHash = userManager.PasswordHasher.HashPassword(verilen.yeniSifre);
            var result = userManager.Update(bulunan);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        [ResponseType(typeof(kullanici))]
        [HttpPost]
        [Route("KullaniciEkle")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IHttpActionResult KullaniciEkle(KayitKalibi verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            kullanici bulunan = (from veri in db.kullanici
                                 where veri.kullaniciadi == verilen.kullaniciadi || veri.Email == verilen.Eposta select veri).FirstOrDefault();

            if (bulunan == null)
            {
                var user = new ApplicationUser();
                user.UserName = verilen.kullaniciadi;
                user.Email = verilen.Eposta;

                var result = userManager.Create(user, verilen.sifre);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                    return Ok(); //200 Ok
                }
                else
                {
                    return BadRequest();
                }

            }
            return Conflict();//409 Conflict
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool kullaniciExists(string id)
        {
            return db.kullanici.Count(e => e.kullaniciadi == id) > 0;
        }
    }
}