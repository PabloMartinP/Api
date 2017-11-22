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
        private Service.OSService _osService = new Service.OSService();
        private Service.ClienteService _clienteService = new Service.ClienteService();
        private Service.FirebaseService fb = new Service.FirebaseService();

        // GET: api/os_status/5
        [ResponseType(typeof(Models.os_statusModel))]
        public IHttpActionResult Getbt_os_status(int os_id)
        {
            //List<bt_os_status> estados = db.bt_os_status.Where(x => x.os_id == os_id).ToList();
            //if (estados == null)
            //{
            //    return NotFound();
            //}

            //List<Models.os_statusModel> modelEstados = Models.os_statusModel.ListConvertTo(estados);
            //Models.os_statusModel ult_estado = modelEstados.OrderByDescending(x => x.timestamp).FirstOrDefault();

            //return Ok(ult_estado);

            Data.bt_os_status ult_estado = _osService.buscarUltEstado(os_id);
            Models.os_statusModel modelEstado = Models.os_statusModel.ConvertTo(ult_estado);

            return Ok(modelEstado);
        }

        
        [ResponseType(typeof(Models.os_statusModel))]
        public IHttpActionResult Postbt_os_status(Models.os_statusModel estado)
        {
            string notificacion_desc, notificacion_texto;
            bt_os_status bt_os_status = Models.os_statusModel.ConvertToBD(estado);
            db.bt_os_status.Add(bt_os_status);
            db.SaveChanges();

            estado.tiempo_sk = bt_os_status.tiempo_sk.ToString("yyyy-MM-dd");
            estado.hh_mm_ss = bt_os_status.hh_mm_ss;
            estado.timestamp = string.Concat(bt_os_status.tiempo_sk.ToString("yyyy-MM-dd"), " ", bt_os_status.hh_mm_ss);

            // Agrego notificacion en caso de que la orden se cierre
            if (estado.estado_sk == 3 || estado.estado_sk == 2)
            {
                // actualizo el estado de cierre de la OT
                Data.bt_solicitudes os = db.bt_solicitudes.Where(x => x.os_id == estado.os_id).FirstOrDefault();

                if(estado.estado_sk == 3)
                {
                    os.fh_cierre = DateTime.Today;
                    db.SaveChanges();

                    notificacion_desc = string.Concat("Solicitud ", estado.os_id.ToString(), " finalizada");
                    notificacion_texto = string.Concat("La solicitud ", estado.os_id.ToString(), " ha sido resuelta, ante cualquier consulta no dude en informarnos");
                }
                else
                {
                    // EN CURSO
                    
                    notificacion_desc = string.Concat("Solicitud ", estado.os_id.ToString(), " en curso");
                    notificacion_texto = "";// string.Concat("El reclamo ", estado.ot_id.ToString(), " esta en curso");
                }
                

                // Doy de alta la notificacion en la LK
                Data.lk_notificacion noti = new Data.lk_notificacion();
                noti.notificacion_desc = notificacion_desc;
                noti.notificacion_texto = notificacion_texto;
                noti.notificacion_tipo = "OT y OS";
                db.lk_notificacion.Add(noti);
                db.SaveChanges();

                // Busco los usuarios del cliente de la OT
                List<usuario> usuarios = _clienteService.findUsersByClient(os.cliente_sk);

                // Armo una notificacion por cada Usuario
                foreach (usuario u in usuarios)
                {
                    Data.bt_notificaciones bt_not = new Data.bt_notificaciones();
                    bt_not.usuario_sk = u.usuario_sk;
                    bt_not.cliente_sk = u.cliente_sk;
                    bt_not.notificacion_sk = noti.notificacion_sk;
                    bt_not.tiempo_sk = DateTime.Today;
                    bt_not.ot_id = estado.os_id;

                    db.bt_notificaciones.Add(bt_not);
                }
                db.SaveChanges();

                // Mando Notificacion Push
                Service.FirebaseService.notificacion_mensaje m = new Service.FirebaseService.notificacion_mensaje();
                m.cliente_sk = os.cliente_sk;
                m.usuario_sk = 0;
                m.titulo = noti.notificacion_desc;
                fb.EnviarAFCM(m);

            }


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