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
using Netmefy.Api.Models;

namespace Netmefy.Api.Controllers.api
{
    public class tecnicosController : ApiController
    {
        TecnicoService _tecnicoService = new TecnicoService();
        /*
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/tecnicos
        public IQueryable<tecnico> Gettecnicos()
        {
            return db.tecnicos;
        }*/

        // GET: api/tecnicos/5
        [ResponseType(typeof(tecnicoInfoModel))]
        public IHttpActionResult Gettecnico(string username)
        {
            tecnico tecnico = _tecnicoService.buscar(username);
            if (tecnico == null)
            {
                return NotFound();
            }

            tecnicoInfoModel tm = new tecnicoInfoModel
            {
                id = tecnico.tecnico_sk,
                nombre = tecnico.tecnico_desc
            };


            return Ok(tm);
        }
        /*
        // PUT: api/tecnicos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttecnico(int id, tecnico tecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tecnico.tecnico_sk)
            {
                return BadRequest();
            }

            db.Entry(tecnico).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tecnicoExists(id))
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

        // POST: api/tecnicos
        [ResponseType(typeof(tecnico))]
        public IHttpActionResult Posttecnico(tecnico tecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tecnicos.Add(tecnico);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tecnicoExists(tecnico.tecnico_sk))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tecnico.tecnico_sk }, tecnico);
        }

        // DELETE: api/tecnicos/5
        [ResponseType(typeof(tecnico))]
        public IHttpActionResult Deletetecnico(int id)
        {
            tecnico tecnico = db.tecnicos.Find(id);
            if (tecnico == null)
            {
                return NotFound();
            }

            db.tecnicos.Remove(tecnico);
            db.SaveChanges();

            return Ok(tecnico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tecnicoExists(int id)
        {
            return db.tecnicos.Count(e => e.tecnico_sk == id) > 0;
        }*/
    }
}