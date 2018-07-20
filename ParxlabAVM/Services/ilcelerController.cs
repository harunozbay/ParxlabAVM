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
    public class ilcelerController : ApiController
    {
        private Model db = new Model();

        [HttpGet]
        // GET: api/ilceler
        public IQueryable<ilce> ilce()
        {
            return db.ilce;
        }

        [HttpGet]
        // GET: api/ilceler/5
        [ResponseType(typeof(ilce))]
        public IHttpActionResult ilce(int id)
        {
            ilce ilce = db.ilce.Find(id);
            if (ilce == null)
            {
                return NotFound();
            }

            return Ok(ilce);
        }

        // PUT: api/ilceler/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putilce(int id, ilce ilce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ilce.ilceid)
            {
                return BadRequest();
            }

            db.Entry(ilce).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ilceExists(id))
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

        // POST: api/ilceler
        [ResponseType(typeof(ilce))]
        public IHttpActionResult Postilce(ilce ilce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ilce.Add(ilce);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ilce.ilceid }, ilce);
        }

        // DELETE: api/ilceler/5
        [ResponseType(typeof(ilce))]
        public IHttpActionResult Deleteilce(int id)
        {
            ilce ilce = db.ilce.Find(id);
            if (ilce == null)
            {
                return NotFound();
            }

            db.ilce.Remove(ilce);
            db.SaveChanges();

            return Ok(ilce);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ilceExists(int id)
        {
            return db.ilce.Count(e => e.ilceid == id) > 0;
        }
    }
}