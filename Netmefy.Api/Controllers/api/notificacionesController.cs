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
    public class notificacionesController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        //// GET: api/notificaciones
        //public IQueryable<bt_notificaciones> Getbt_notificaciones()
        //{
        //    return db.bt_notificaciones;
        //}

        // GET: api/notificaciones/5
        [ResponseType(typeof(Models.notificacionesModel))]
        public IHttpActionResult Getbt_notificaciones(int cliente_sk,int usuario_sk)
        {
            Service.NotificacionesService ns = new Service.NotificacionesService();

            List<bt_notificaciones> notificaciones = ns.buscarNotificacionesXClienteUsuario(cliente_sk,usuario_sk);

            List<Models.notificacionesModel> not_model = Models.notificacionesModel.ConvertTo(notificaciones);

            return Ok(not_model);
        }

        //[ResponseType(typeof(bt_notificaciones))]
        //public IHttpActionResult Getbt_notificaciones(int cliente_sk, int usuario_sk)
        //{
        //    Service.NotificacionesService ns = new Service.NotificacionesService();

        //    var notificaciones = ns.buscarNotificacionesXClienteUsuario(cliente_sk, usuario_sk);

        //    return Ok(notificaciones);
        //}

        //[ResponseType(typeof(bt_notificaciones))]
        //public IHttpActionResult Getbt_notificaciones(int cliente_sk)
        //{
        //    bt_notificaciones bt_notificaciones = db.bt_notificaciones.Find(id);
        //    if (bt_notificaciones == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(bt_notificaciones);
        //}

        //// PUT: api/notificaciones/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putbt_notificaciones(int id, bt_notificaciones bt_notificaciones)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != bt_notificaciones.usuario_sk)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(bt_notificaciones).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!bt_notificacionesExists(id))
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

        //// POST: api/notificaciones
        //[ResponseType(typeof(bt_notificaciones))]
        //public IHttpActionResult Postbt_notificaciones(bt_notificaciones bt_notificaciones)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.bt_notificaciones.Add(bt_notificaciones);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (bt_notificacionesExists(bt_notificaciones.usuario_sk))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = bt_notificaciones.usuario_sk }, bt_notificaciones);
        //}

        //// DELETE: api/notificaciones/5
        //[ResponseType(typeof(bt_notificaciones))]
        //public IHttpActionResult Deletebt_notificaciones(int id)
        //{
        //    bt_notificaciones bt_notificaciones = db.bt_notificaciones.Find(id);
        //    if (bt_notificaciones == null)
        //    {
        //        return NotFound();
        //    }

        //    db.bt_notificaciones.Remove(bt_notificaciones);
        //    db.SaveChanges();

        //    return Ok(bt_notificaciones);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool bt_notificacionesExists(int id)
        //{
        //    return db.bt_notificaciones.Count(e => e.usuario_sk == id) > 0;
        //}
    }
}