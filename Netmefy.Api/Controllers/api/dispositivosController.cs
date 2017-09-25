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


        // POST: api/solicitudes
        [ResponseType(typeof(dispositivo))]
        public IHttpActionResult Postdispositivo (dispositivo dispositivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dispositivo.dispositivo_sk == 0)
            {
                db.dispositivos.Add(dispositivo);

                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = dispositivo.cliente_sk }, dispositivo);

            } else
            {
                dispositivo disp = db.dispositivos.Where(x => x.dispositivo_sk == dispositivo.dispositivo_sk).FirstOrDefault();

                disp.dispositivo_bloq = dispositivo.dispositivo_bloq;
                disp.dispositivo_apodo = dispositivo.dispositivo_apodo;
                disp.dispositivo_foto = dispositivo.dispositivo_foto;
                disp.dispositivo_ip = dispositivo.dispositivo_ip;
                disp.dispositivo_mac = dispositivo.dispositivo_mac;
                disp.dispositivo_tipo = dispositivo.dispositivo_tipo;

                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = disp.cliente_sk }, dispositivo);
            }
            

            
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