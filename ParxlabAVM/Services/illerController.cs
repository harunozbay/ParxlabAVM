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
    public class illerController : ApiController
    {
        private Model db = new Model();

        [HttpGet]
        // GET: api/iller
        public IQueryable<il> iller()
        {
            return db.il;
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ilExists(int id)
        {
            return db.il.Count(e => e.plaka == id) > 0;
        }
    }
}