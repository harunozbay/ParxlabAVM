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
    public class cihazlarController : ApiController
    {
        private Model db = new Model();

        
        // GET: api/cihazlar/5
        [ResponseType(typeof(IQueryable<cihaz>))]
        [HttpGet]
        public IHttpActionResult cihaz(int id)
        {
            IQueryable<cihaz> cihazlar = from cihaz in db.cihaz where cihaz.parkid == id select cihaz;
            if (cihazlar == null)
            {
                return NotFound();
            }

            return Ok(cihazlar);
        }
        
    }
}