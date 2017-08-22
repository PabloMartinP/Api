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
using Netmefy.Service;

namespace Netmefy.Api.Controllers.api
{
    public class tokensController : ApiController
    {
        /*
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/tokens
        public IQueryable<token> Gettokens()
        {
            return db.tokens;
        }

        // GET: api/tokens/5
        [ResponseType(typeof(token))]
        public IHttpActionResult Gettoken(int id)
        {
            token token = db.tokens.Find(id);
            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }

        // PUT: api/tokens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttoken(int id, token token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != token.id)
            {
                return BadRequest();
            }

            db.Entry(token).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tokenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }*/

        // POST: api/tokens
        [ResponseType(typeof(token))]
        public IHttpActionResult Posttoken(token token)
        {
            FirebaseService fb = new FirebaseService();
            fb.registerToken(token);
            
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.tokens.Add(token);
            db.SaveChanges();*/
            return CreatedAtRoute("DefaultApi", new { id = token.id }, token);
        }
        /*
        // DELETE: api/tokens/5
        [ResponseType(typeof(token))]
        public IHttpActionResult Deletetoken(int id)
        {
            token token = db.tokens.Find(id);
            if (token == null)
            {
                return NotFound();
            }

            db.tokens.Remove(token);
            db.SaveChanges();

            return Ok(token);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tokenExists(int id)
        {
            return db.tokens.Count(e => e.id == id) > 0;
        }*/
    }
}