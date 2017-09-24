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

        // GET: api/solicitudes/5
        [ResponseType(typeof(bt_solicitudes))]
        public IHttpActionResult Getbt_solicitudes(int id)
        {
            bt_solicitudes bt_solicitudes = db.bt_solicitudes.Find(id);
            if (bt_solicitudes == null)
            {
                return NotFound();
            }

            return Ok(bt_solicitudes);
        }

        [ResponseType(typeof(Models.notificacionesModel))]
        public IHttpActionResult Getbt_notificaciones(int cliente_sk, int usuario_sk)
        {
            Service.NotificacionesService ns = new Service.NotificacionesService();

            List<bt_notificaciones> notificaciones = ns.buscarNotificacionesXClienteUsuario(cliente_sk, usuario_sk);

            List<Models.notificacionesModel> not_model = Models.notificacionesModel.ConvertTo(notificaciones);

            return Ok(not_model);
        }

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

        //// POST: api/solicitudes
        //[ResponseType(typeof(bt_solicitudes))]
        //public IHttpActionResult Postbt_solicitudes(bt_solicitudes bt_solicitudes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.bt_solicitudes.Add(bt_solicitudes);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (bt_solicitudesExists(bt_solicitudes.os_id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = bt_solicitudes.os_id }, bt_solicitudes);
        //}

        //// DELETE: api/solicitudes/5
        //[ResponseType(typeof(bt_solicitudes))]
        //public IHttpActionResult Deletebt_solicitudes(int id)
        //{
        //    bt_solicitudes bt_solicitudes = db.bt_solicitudes.Find(id);
        //    if (bt_solicitudes == null)
        //    {
        //        return NotFound();
        //    }

        //    db.bt_solicitudes.Remove(bt_solicitudes);
        //    db.SaveChanges();

        //    return Ok(bt_solicitudes);
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