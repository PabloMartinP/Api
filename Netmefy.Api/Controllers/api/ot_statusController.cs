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
    public class ot_statusController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ot_status/5
        [ResponseType(typeof(Models.ot_statusModel))]
        public IHttpActionResult Getbt_ot_status(int ot_id)
        {
            List<bt_ot_status> estados = db.bt_ot_status.Where(x => x.ot_id == ot_id).ToList();
            if (estados == null)
            {
                return NotFound();
            }

            List<Models.ot_statusModel> modelEstados = Models.ot_statusModel.ListConvertTo(estados);
            Models.ot_statusModel ult_estado = modelEstados.OrderByDescending(x => x.timestamp).FirstOrDefault();

            return Ok(ult_estado);
        }


        [ResponseType(typeof(Models.ot_statusModel))]
        public IHttpActionResult Postbt_ot_status(Models.ot_statusModel estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bt_ot_status bt_ot_status = Models.ot_statusModel.ConvertToBD(estado);
            db.bt_ot_status.Add(bt_ot_status);
            db.SaveChanges();

            estado.tiempo_sk = bt_ot_status.tiempo_sk.ToString("dd-MM-yyyy");
            estado.hh_mm_ss = bt_ot_status.hh_mm_ss;
            estado.timestamp = string.Concat(bt_ot_status.tiempo_sk.ToString("yyyy-dd-MM"), " ", bt_ot_status.hh_mm_ss);

            return CreatedAtRoute("DefaultApi", new { id = estado.ot_id }, estado);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bt_ot_statusExists(int id)
        {
            return db.bt_ot_status.Count(e => e.ot_id == id) > 0;
        }
    }
}