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
using Netmefy.Data;

namespace Netmefy.Api.Controllers.api
{
    public class os_statusController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/os_status
        public IQueryable<bt_os_status> Getbt_os_status()
        {
            return db.bt_os_status;
        }

        // GET: api/os_status/5
        [ResponseType(typeof(bt_os_status))]
        public IHttpActionResult Getbt_os_status(int id)
        {
            bt_os_status bt_os_status = db.bt_os_status.Find(id);
            if (bt_os_status == null)
            {
                return NotFound();
            }

            return Ok(bt_os_status);
        }

        // PUT: api/os_status/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbt_os_status(int id, bt_os_status bt_os_status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bt_os_status.os_id)
            {
                return BadRequest();
            }

            db.Entry(bt_os_status).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bt_os_statusExists(id))
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

        // POST: api/os_status
        [ResponseType(typeof(bt_os_status))]
        public IHttpActionResult Postbt_os_status(bt_os_status bt_os_status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bt_os_status.Add(bt_os_status);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (bt_os_statusExists(bt_os_status.os_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bt_os_status.os_id }, bt_os_status);
        }

        // DELETE: api/os_status/5
        [ResponseType(typeof(bt_os_status))]
        public IHttpActionResult Deletebt_os_status(int id)
        {
            bt_os_status bt_os_status = db.bt_os_status.Find(id);
            if (bt_os_status == null)
            {
                return NotFound();
            }

            db.bt_os_status.Remove(bt_os_status);
            db.SaveChanges();

            return Ok(bt_os_status);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bt_os_statusExists(int id)
        {
            return db.bt_os_status.Count(e => e.os_id == id) > 0;
        }
    }
}