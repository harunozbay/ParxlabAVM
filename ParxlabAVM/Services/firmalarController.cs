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
    public class firmalarController : ApiController
    {
        private Model db = new Model();

        [HttpGet]
        // GET: api/firmalar
        public IQueryable<firma> firmalar()
        {
            return db.firma;
        }

        [HttpGet]
        // GET: api/firmalar/5
        [ResponseType(typeof(IQueryable<firma>))]
        public IHttpActionResult firmalar(int id)
        {
            IQueryable<firma> firma = (from veri in db.firma where veri.ilceid == id select veri);
            if (firma == null)
            {
                return NotFound();
            }

            return Ok(firma);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool firmaExists(int id)
        {
            return db.firma.Count(e => e.firmaid == id) > 0;
        }
    }
}