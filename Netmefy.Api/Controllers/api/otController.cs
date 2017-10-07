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
    public class otController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();


        public List<Data.bt_ord_trabajo> buscarOtXCliente(int cliente_sk)
        {
            var j = db.bt_ord_trabajo.Where(x => x.cliente_sk == cliente_sk).ToList();
            return j;
        }

        // GET: api/ordenes/5
        [ResponseType(typeof(Models.otModel))]
        public IHttpActionResult Getbt_ord_trabajo(int cliente_sk)
        {
            List<bt_ord_trabajo> ots = buscarOtXCliente(cliente_sk);

            List<Models.otModel> ordenes = Models.otModel.ListConvertTo(ots);

            return Ok(ordenes);
        }

        // POST: api/ordenes
        [ResponseType(typeof(Models.otModel))]        
        public IHttpActionResult Postbt_ord_trabajo(Models.otModel orden)
        {
            bt_ord_trabajo bt_ord_trabajo = Models.otModel.ConvertToBD(orden);

            if (bt_ord_trabajo.ot_id == 0)
            {
                db.bt_ord_trabajo.Add(bt_ord_trabajo);

                db.SaveChanges();
                orden.fh_cierre = ((DateTime)(bt_ord_trabajo.fh_cierre)).ToString("yyyy-mm-dd"); ;
                orden.fh_creacion = ((DateTime)(bt_ord_trabajo.fh_creacion)).ToString("yyyy-mm-dd"); ;
                orden.ot_id = bt_ord_trabajo.ot_id;

                return CreatedAtRoute("DefaultApi", new { id = bt_ord_trabajo.ot_id }, orden);

            }
            else
            {
                bt_ord_trabajo ord_trabajo = db.bt_ord_trabajo.Where(x => x.ot_id == bt_ord_trabajo.ot_id).FirstOrDefault();

                ord_trabajo.fh_creacion = bt_ord_trabajo.fh_creacion;
                ord_trabajo.fh_cierre = bt_ord_trabajo.fh_cierre;
                ord_trabajo.calificacion = bt_ord_trabajo.calificacion;
                ord_trabajo.tecnico_sk = bt_ord_trabajo.tecnico_sk;
                ord_trabajo.tipo = bt_ord_trabajo.tipo;
                ord_trabajo.descripcion = bt_ord_trabajo.descripcion;

                db.SaveChanges();
                orden.fh_creacion = ((DateTime)(ord_trabajo.fh_creacion)).ToString("yyyy-mm-dd"); ;
                orden.fh_cierre = ((DateTime)(ord_trabajo.fh_cierre)).ToString("yyyy-mm-dd"); ;

                return CreatedAtRoute("DefaultApi", new { id = ord_trabajo.ot_id }, orden);
            }

        }
        // PUT: api/paginas/5
        //[ResponseType(typeof(string))]
        [HttpPut]
        public IHttpActionResult updatearCalificacion(int id, int calificacion)
        {

            try
            {
                bt_ord_trabajo orden = db.bt_ord_trabajo.Where(x => x.ot_id == id).FirstOrDefault();
                orden.calificacion = calificacion;

                db.SaveChanges();

                Models.otModel bt_ord_trabajo = Models.otModel.ConvertTo(orden);
                //return StatusCode(HttpStatusCode.NoContent);
                return CreatedAtRoute("DefaultApi", new { status = "ok" }, new { ot = bt_ord_trabajo });
            }
            catch (Exception ex)
            {
                return CreatedAtRoute("DefaultApi", new { status = "error" }, new { mensaje=  ex.ToString() });
            }
        }


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