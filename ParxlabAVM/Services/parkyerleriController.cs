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
    public class parkyerleriController : ApiController
    {
        private Model db = new Model();

        [HttpGet]
        // GET: api/parkyerleri
        public IQueryable<parkyeri> parkyerleri()
        {
            return db.parkyeri;
        }

        [HttpGet]
        // GET: api/parkyerleri/5
        [ResponseType(typeof(IQueryable<parkyeri>))]
        public IHttpActionResult parkyerleri(int id)
        {
            IQueryable<parkyeri> parkyeri = (from veri in db.parkyeri where veri.firmaid == id select veri);
            if (parkyeri == null)
            {
                return NotFound();
            }

            return Ok(parkyeri);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool parkyeriExists(int id)
        {
            return db.parkyeri.Count(e => e.parkid == id) > 0;
        }
    }
}