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
    public class os_statusController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        
        // GET: api/os_status/5
        [ResponseType(typeof(Models.os_statusModel))]
        public IHttpActionResult Getbt_os_status(int os_id)
        {
            List<bt_os_status> estados = db.bt_os_status.Where(x => x.os_id == os_id).ToList();
            if (estados == null)
            {
                return NotFound();
            }

            List<Models.os_statusModel> modelEstados = Models.os_statusModel.ListConvertTo(estados);
            Models.os_statusModel ult_estado = modelEstados.OrderByDescending(x => x.timestamp).FirstOrDefault();

            return Ok(ult_estado);
        }

        
        [ResponseType(typeof(Models.os_statusModel))]
        public IHttpActionResult Postbt_os_status(Models.os_statusModel estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bt_os_status bt_os_status = Models.os_statusModel.ConvertToBD(estado);
            db.bt_os_status.Add(bt_os_status);
            db.SaveChanges();

            estado.tiempo_sk = bt_os_status.tiempo_sk.ToString("dd-MM-yyyy");
            estado.hh_mm_ss = bt_os_status.hh_mm_ss;
            estado.timestamp = string.Concat(bt_os_status.tiempo_sk.ToString("yyyy-dd-MM"), " ", bt_os_status.hh_mm_ss);
                
            return CreatedAtRoute("DefaultApi", new { id = estado.os_id }, estado);
            
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bt_os_statusExists(int id)
        {
            return db.bt_os_status.Count(e => e.os_id == id) > 0;
        }
    }
}