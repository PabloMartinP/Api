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
        private Service.OTService _otService = new Service.OTService();
        private Service.ClienteService _clienteService = new Service.ClienteService();

        // GET: api/ot_status/5
        [ResponseType(typeof(Models.ot_statusModel))]
        public IHttpActionResult Getbt_ot_status(int ot_id)
        {
            //List<bt_ot_status> estados = db.bt_ot_status.Where(x => x.ot_id == ot_id).ToList();
            //if (estados == null)
            //{
            //    return NotFound();
            //}

            //List<Models.ot_statusModel> modelEstados = Models.ot_statusModel.ListConvertTo(estados);
            //Models.ot_statusModel ult_estado = modelEstados.OrderByDescending(x => x.timestamp).FirstOrDefault();

            Data.bt_ot_status ult_estado = _otService.buscarUltEstado(ot_id);
            Models.ot_statusModel modelEstado = Models.ot_statusModel.ConvertTo(ult_estado);

            return Ok(modelEstado);
        }


        [ResponseType(typeof(Models.ot_statusModel))]
        public IHttpActionResult Postbt_ot_status(Models.ot_statusModel estado)
        {
            bt_ot_status bt_ot_status = Models.ot_statusModel.ConvertToBD(estado);
            db.bt_ot_status.Add(bt_ot_status);
            db.SaveChanges();

            estado.tiempo_sk = bt_ot_status.tiempo_sk.ToString("yyyy-MM-dd");
            estado.hh_mm_ss = bt_ot_status.hh_mm_ss;
            estado.timestamp = string.Concat(bt_ot_status.tiempo_sk.ToString("yyyy-MM-dd"), " ", bt_ot_status.hh_mm_ss);

            // Agrego notificacion en caso de que la orden se cierre
            if(estado.estado_sk == 3)
            {
                // Doy de alta la notificacion en la LK
                Data.lk_notificacion noti = new Data.lk_notificacion();
                noti.notificacion_desc = string.Concat("Reclamo ",estado.ot_id.ToString()," finalizado");
                noti.notificacion_texto = string.Concat("El reclamo ", estado.ot_id.ToString(), " ha sido resuelto, ante cualquier consulta no dude en informarnos");
                db.lk_notificacion.Add(noti);

                // Busco los usuarios del cliente de la OT
                Data.bt_ord_trabajo ot = _otService.buscarOtXOTID(estado.ot_id);
                List<usuario> usuarios = _clienteService.findUsersByClient(ot.cliente_sk);

                // Armo una notificacion por cada Usuario
                foreach (usuario u in usuarios)
                {
                    Data.bt_notificaciones bt_not = new Data.bt_notificaciones();
                    bt_not.usuario_sk = u.usuario_sk;
                    bt_not.cliente_sk = u.cliente_sk;
                    bt_not.notificacion_sk = noti.notificacion_sk;
                    bt_not.tiempo_sk = DateTime.Today;
                    bt_not.ot_id = estado.ot_id;

                    db.bt_notificaciones.Add(bt_not);
                    db.SaveChanges();
                }
                
            }

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