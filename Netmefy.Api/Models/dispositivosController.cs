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

namespace Netmefy.Api.Models
{
    public class dispositivosController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/dispositivos
        public IQueryable<dispositivo> Getdispositivos()
        {
            return db.dispositivos;
        }

        // GET: api/dispositivos/5
        [ResponseType(typeof(dispositivo))]
        public IHttpActionResult Getdispositivo(int id)
        {
            dispositivo dispositivo = db.dispositivos.Find(id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            return Ok(dispositivo);
        }

        private bool dispositivoExists(int id)
        {
            return db.dispositivos.Count(e => e.cliente_sk == id) > 0;
        }


        // POST: api/dispositivos
        [ResponseType(typeof(dispositivo))]
        public IHttpActionResult Postdispositivo(dispositivo dispositivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.dispositivos.Add(dispositivo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (dispositivoExists(dispositivo.cliente_sk))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dispositivo.cliente_sk }, dispositivo);
        }

        // PUT: api/dispositivos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdispositivo(int id, dispositivo dispositivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dispositivo.cliente_sk)
            {
                return BadRequest();
            }

            db.Entry(dispositivo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dispositivoExists(id))
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

        public IHttpActionResult PutPushdispositivo
            (int cliente_sk, int router_sk, int dispositivo_sk, string dispositivo_mac, string dispositivo_ip, 
             int dispositivo_bloq, string dispositivo_tipo, string dispositivo_apodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dispositivo_sk == 0)
            {
                dispositivo new_disp = new dispositivo
                {
                    cliente_sk = cliente_sk,
                    router_sk = router_sk,
                    dispositivo_mac = dispositivo_mac,
                    dispositivo_ip = dispositivo_ip,
                    dispositivo_bloq = dispositivo_bloq,
                    dispositivo_tipo = dispositivo_tipo,
                    dispositivo_apodo = dispositivo_apodo
                };

                db.dispositivos.Add(new_disp);

            } else
            {
                dispositivo dispositivo = db.dispositivos.Find(dispositivo_sk);

                dispositivo.dispositivo_mac = dispositivo_mac;
                dispositivo.dispositivo_ip = dispositivo_ip;
                dispositivo.dispositivo_bloq = dispositivo_bloq;
                dispositivo.dispositivo_tipo = dispositivo_tipo;
                dispositivo.dispositivo_apodo = dispositivo_apodo;
            }
            db.SaveChanges();
        }

        //// DELETE: api/dispositivos/5
        //[ResponseType(typeof(dispositivo))]
        //public IHttpActionResult Deletedispositivo(int id)
        //{
        //    dispositivo dispositivo = db.dispositivos.Find(id);
        //    if (dispositivo == null)
        //    {
        //        return NotFound();
        //    }

        //    db.dispositivos.Remove(dispositivo);
        //    db.SaveChanges();

        //    return Ok(dispositivo);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


    }
    }