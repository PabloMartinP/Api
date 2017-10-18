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
        private Service.FirebaseService fb = new Service.FirebaseService();


        // GET: api/notificaciones/5
        [ResponseType(typeof(Models.notificacionesModel))]
        public IHttpActionResult Getbt_notificaciones(int cliente_sk,int usuario_sk)
        {
            Service.NotificacionesService ns = new Service.NotificacionesService();

            List<bt_notificaciones> notificaciones = ns.buscarNotificacionesXClienteUsuario(cliente_sk,usuario_sk);

            List<Models.notificacionesModel> not_model = Models.notificacionesModel.ConvertTo(notificaciones);

            foreach (var item in not_model)
            {
                Service.OTService ots = new Service.OTService();

                if (item.ot_id != null && item.ot_id != 0) {
                    decimal? calif = ots.buscarOtXOTID((int)item.ot_id).calificacion;
                    if (calif == null)
                        item.ot_calificacion = 0;
                    else
                        item.ot_calificacion = (decimal)calif;

                }

            }

            return Ok(not_model);
        }



        // POST: api/notificaciones
        [ResponseType(typeof(Models.notificacionesModel))]
        public IHttpActionResult Postbt_notificaciones(Models.notificacionesModel n)
        {
            if(n.notificacion_sk == 0)
            {
                // Creo en el maestro la notificacion
                Data.lk_notificacion noti = new Data.lk_notificacion();
                noti.notificacion_desc = n.notificacion_desc;
                noti.notificacion_texto = n.notificacion_texto;
                db.lk_notificacion.Add(noti);
                db.SaveChanges();
                n.notificacion_sk = noti.notificacion_sk;
            }

            // La agrego en la BT_Notificaciones
            Data.bt_notificaciones not = new Data.bt_notificaciones();
            not = Models.notificacionesModel.ConvertToBD(n);
            db.bt_notificaciones.Add(not);
            db.SaveChanges();


            // Envio notificacion Push
            Service.FirebaseService.notificacion_mensaje m = new Service.FirebaseService.notificacion_mensaje();
            m.cliente_sk = 0;
            m.usuario_sk = n.usuario_sk;
            m.descripcion = n.notificacion_texto;
            m.titulo =n.notificacion_desc;
            fb.EnviarAFCM(m);

            return CreatedAtRoute("DefaultApi", new { id = n.usuario_sk }, n);
        }
    }
}