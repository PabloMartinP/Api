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
        

        // GET: api/notificaciones/5
        [ResponseType(typeof(Models.notificacionesModel))]
        public IHttpActionResult Getbt_notificaciones(int cliente_sk,int usuario_sk)
        {
            Service.NotificacionesService ns = new Service.NotificacionesService();

            List<bt_notificaciones> notificaciones = ns.buscarNotificacionesXClienteUsuario(cliente_sk,usuario_sk);

            List<Models.notificacionesModel> not_model = Models.notificacionesModel.ConvertTo(notificaciones);

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
            

            return CreatedAtRoute("DefaultApi", new { id = n.usuario_sk }, n);
        }
    }
}