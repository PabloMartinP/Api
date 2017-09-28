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
using System.Web.Script.Serialization;

namespace Netmefy.Api.Controllers.api
{
    public class paginasController : ApiController
    {

        private NETMEFYEntities db = new NETMEFYEntities();
        /*
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
        }*/

        // POST: api/paginas
        [ResponseType(typeof(nuevaPaginaModel))]
        [HttpPost]
        public IHttpActionResult nuevaPagina(nuevaPaginaModel paginas)
        {
            paginas.id = 5678;
            return CreatedAtRoute("DefaultApi", new { id = 1234 }, paginas);
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            //var client_found = clienteService.findUserById(pagina.cliente_sk, pagina.usuario_sk);
            var client_found = db.usuarios.Where(x => x.cliente_sk == paginas.cliente_sk && x.usuario_sk == paginas.usuario_sk).FirstOrDefault();
            //db.paginas.Add(pagina);
            //db.SaveChanges();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> paginasstring = serializer.Deserialize<List<string>>(paginas.paginas);

            

            foreach (string pagina in paginasstring)
            {
                pagina p = new pagina
                {
                    entidad_desc = pagina.Replace("_63_", "?").Replace("_38_","&")
                };
                client_found.paginas.Add(p);
            }

            
            //db.paginas.Add(pagina);

            db.SaveChanges();
            

            return CreatedAtRoute("DefaultApi", new { id = paginas.id }, paginas);*/
        }
        /*
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
        */
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