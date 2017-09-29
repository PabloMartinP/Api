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

namespace Netmefy.Api.Controllers
{
    public class solicitudesController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        //// GET: api/solicitudes
        //public IQueryable<bt_solicitudes> Getbt_solicitudes()
        //{
        //    return db.bt_solicitudes;
        //}

        public List<Data.bt_solicitudes> buscarSolicitudesXCliente(int cliente_sk)
        {
            var j = db.bt_solicitudes.Where(x => x.cliente_sk == cliente_sk).ToList();
            return j;
        }

        // GET: api/solicitudes/5
        [ResponseType(typeof(Models.solicitudesModel))]
        public IHttpActionResult Getbt_solicitudes(int cliente_sk)
        {
            List<bt_solicitudes> solicitudes = buscarSolicitudesXCliente(cliente_sk);

            List<Models.solicitudesModel> solpe = Models.solicitudesModel.ListConvertTo(solicitudes);

            return Ok(solpe);
        }

        // POST: api/solicitudes
        [ResponseType(typeof(Models.solicitudesModel))]        
        //public IHttpActionResult Postbt_solicitudes(Models.solicitudesModel solicitud)
        public IHttpActionResult Postbt_solicitudes(Models.solicitudesModel solicitud)
        {
            
            bt_solicitudes bt_solicitudes = Models.solicitudesModel.ConvertToBD(solicitud);

            if (bt_solicitudes.os_id == 0)
            {
                db.bt_solicitudes.Add(bt_solicitudes);

                db.SaveChanges();
                solicitud.fh_cierre = ((DateTime)(bt_solicitudes.fh_cierre)).ToString("yyyy-mm-dd"); ;
                solicitud.fh_creacion = ((DateTime)(bt_solicitudes.fh_creacion)).ToString("yyyy-mm-dd"); ;
                solicitud.os_id = bt_solicitudes.os_id;

                return CreatedAtRoute("DefaultApi", new { id = bt_solicitudes.os_id }, solicitud);

            }
            else
            {
                bt_solicitudes solpe = db.bt_solicitudes.Where(x => x.os_id == bt_solicitudes.os_id && x.cliente_sk == bt_solicitudes.cliente_sk).FirstOrDefault();

                solpe.fh_creacion = bt_solicitudes.fh_creacion;
                solpe.fh_cierre = bt_solicitudes.fh_cierre;

                db.SaveChanges();
                solicitud.fh_creacion = ((DateTime)(solpe.fh_creacion)).ToString("yyyy-mm-dd"); ;
                solicitud.fh_cierre = ((DateTime)(solpe.fh_cierre)).ToString("yyyy-mm-dd"); ;

                return CreatedAtRoute("DefaultApi", new { id = solpe.os_id }, solicitud);
            }

        }

        //// DELETE: api/solicitudes/5
        //[ResponseType(typeof(bt_solicitudes))]
        //public IHttpActionResult Deletebt_solicitudes(int id)
        //{
        //    bt_solicitudes bt_solicitudes = db.bt_solicitudes.Find(id);
        //    if  (bt_solicitudes == null)
        //    {
        //        return NotFound();
        //    }

        //    db.bt_solicitudes.Remove(bt_solicitudes);
        //    db.SaveChanges();

        //    return Ok(bt_solicitudes);
        //}


        //// PUT: api/solicitudes/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putbt_solicitudes(int id, bt_solicitudes bt_solicitudes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != bt_solicitudes.os_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(bt_solicitudes).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!bt_solicitudesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bt_solicitudesExists(int id)
        {
            return db.bt_solicitudes.Count(e => e.os_id == id) > 0;
        }
    }
}