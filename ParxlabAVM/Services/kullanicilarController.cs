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
    public class kullanicilarController : ApiController
    {
        private Model db = new Model();

        public IQueryable<kullanici> Getkullanici()
        {
            return db.kullanici;
        }

        // PUT: api/kullanicilar/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putkullanici(string id, kullanici kullanici)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kullanici.kullaniciid)
            {
                return BadRequest();
            }

            db.Entry(kullanici).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!kullaniciExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/kullanicilar
        [ResponseType(typeof(kullanici))]
        [HttpPost]
        public IHttpActionResult KullaniciDogrula(IdSifreIkilisi kullanici)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            kullanici bulunan = (from veri in db.kullanici
                                 where veri.kullaniciid == kullanici.kullaniciid && veri.sifre == kullanici.sifre
                                 select veri).FirstOrDefault();

            if (bulunan == null)
            {
                return NotFound();
            }
            return Ok(bulunan);
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
            return db.kullanici.Count(e => e.kullaniciid == id) > 0;
        }
    }
}