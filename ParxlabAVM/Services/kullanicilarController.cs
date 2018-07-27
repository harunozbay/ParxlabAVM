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
using Microsoft.AspNet.Identity;
using ParxlabAVM.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ParxlabAVM.Services
{
    [RoutePrefix("api/kullanicilar")]
    public class kullanicilarController : ApiController
    {
        private Model db = new Model();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));

        public IQueryable<kullanici> Getkullanici()
        {
            return db.AspNetUsers;
        }



        // POST: api/kullanicilar
        [Route("KullaniciDogrula")]
        [ResponseType(typeof(kullanici))]
        [HttpPost]
        public IHttpActionResult KullaniciDogrula(IdSifreIkilisi verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bulunan = userManager.Find(verilen.kullaniciid, verilen.sifre);

            if (bulunan == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("SifreDegistir")]
        [ResponseType(typeof(kullanici))]
        [HttpPost]
        public IHttpActionResult SifreDegistir(IdEskiYeniSifre verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bulunan = userManager.Find(verilen.kullaniciid, verilen.eskiSifre);

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
        public IHttpActionResult KullaniciEkle(Register verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            kullanici bulunan = (from veri in db.AspNetUsers
                                 where veri.kullaniciadi == verilen.kullaniciid select veri).FirstOrDefault();

            if (bulunan == null)
            {
                var user = new ApplicationUser();
                user.UserName = verilen.kullaniciid;
                user.Email = verilen.Email;

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
            return db.AspNetUsers.Count(e => e.kullaniciadi == id) > 0;
        }
    }
}