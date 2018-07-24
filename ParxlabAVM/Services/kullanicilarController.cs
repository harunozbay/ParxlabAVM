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

namespace ParxlabAVM.Services
{
    [RoutePrefix("api/kullanicilar")]
    public class kullanicilarController : ApiController
    {
        private Model db = new Model();

        public IQueryable<AspNetUsers> Getkullanici()
        {
            return db.AspNetUsers;
        }



        // POST: api/kullanicilar
        [Route("KullaniciDogrula")]
        [ResponseType(typeof(AspNetUsers))]
        [HttpPost]
        public IHttpActionResult KullaniciDogrula(IdSifreIkilisi verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetUsers bulunan = (from veri in db.AspNetUsers
                                 where veri.UserName == @verilen.kullaniciid && veri.PasswordHash == verilen.sifre
                                 select veri).FirstOrDefault();

            if (bulunan == null)
            {
                return NotFound();
            }
            return Ok(bulunan);
        }

        [Route("SifreDegistir")]
        [ResponseType(typeof(AspNetUsers))]
        [HttpPost]
        public IHttpActionResult SifreDegistir(IdEskiYeniSifre verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetUsers bulunan = (from veri in db.AspNetUsers
                                 where veri.UserName == verilen.kullaniciid && veri.PasswordHash == verilen.eskiSifre
                                 select veri).FirstOrDefault();

            if (bulunan == null)
            {
                return NotFound();
            }
            bulunan.PasswordHash = verilen.yeniSifre;
            db.SaveChanges();
            return Ok(bulunan);
        }

        [ResponseType(typeof(AspNetUsers))]
        [HttpPost]
        [Route("KullaniciEkle")]
        public IHttpActionResult KullaniciEkle(AspNetUsers verilen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetUsers bulunan = (from veri in db.AspNetUsers
                                 where veri.UserName == verilen.UserName select veri).FirstOrDefault();

            if (bulunan == null)
            {
                db.AspNetUsers.Add(verilen);
                db.SaveChanges();
                return Ok();// 200 Ok
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
            return db.AspNetUsers.Count(e => e.UserName == id) > 0;
        }
    }
}