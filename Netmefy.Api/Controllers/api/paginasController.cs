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
    public class paginasController : ApiController
    {

        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/paginas
        public IQueryable<pagina> Getpaginas()
        {
            Data.pagina p = new pagina();
            
            return db.paginas;
        }

        // GET: api/paginas/5
        [ResponseType(typeof(pagina))]
        public IHttpActionResult Getpagina(int id)
        {
            pagina pagina = db.paginas.Find(id);
            if (pagina == null)
            {
                return NotFound();
            }

            return Ok(pagina);
        }

        // PUT: api/paginas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpagina(int id, pagina pagina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pagina.entidad_sk)
            {
                return BadRequest();
            }

            db.Entry(pagina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!paginaExists(id))
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

        // POST: api/paginas
        [ResponseType(typeof(pagina))]
        public IHttpActionResult Postpagina(pagina pagina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.paginas.Add(pagina);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (paginaExists(pagina.entidad_sk))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pagina.entidad_sk }, pagina);
        }

        // DELETE: api/paginas/5
        [ResponseType(typeof(pagina))]
        public IHttpActionResult Deletepagina(int id)
        {
            pagina pagina = db.paginas.Find(id);
            if (pagina == null)
            {
                return NotFound();
            }

            db.paginas.Remove(pagina);
            db.SaveChanges();

            return Ok(pagina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool paginaExists(int id)
        {
            return db.paginas.Count(e => e.entidad_sk == id) > 0;
        }
    }
}