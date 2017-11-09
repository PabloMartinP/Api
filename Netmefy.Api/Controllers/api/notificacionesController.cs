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
using Netmefy.Api.Models;

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

        [HttpPut]
        public IHttpActionResult marcarComoLeido(int id, int cliente_sk, int usuario_sk)
        {
            bt_notificaciones n = null;
            Models.notificacionesModel nm = new notificacionesModel();
            nm.notificacion_sk = id;
            nm.cliente_sk = cliente_sk;
            nm.usuario_sk = usuario_sk;
            try
            {
                n = db.bt_notificaciones.Where(x => x.notificacion_sk == id && x.cliente_sk == cliente_sk && x.usuario_sk == usuario_sk).FirstOrDefault();
                n.leido = true;
                db.SaveChanges();
                nm = new notificacionesModel
                {
                    usuario_sk = n.usuario_sk,
                    cliente_sk = n.cliente_sk,
                    notificacion_sk = n.notificacion_sk,
                    tiempo_sk = n.tiempo_sk.ToString("yyyy-MM-dd"),
                    ot_id = n.ot_id,
                    notificacion_desc = "", 
                    notificacion_texto = ""
                };

                //return StatusCode(HttpStatusCode.NoContent);
                return CreatedAtRoute("DefaultApi", new { status = "ok" }, new {status="ok", noti = nm });
            }
            catch (Exception ex)
            {

                return CreatedAtRoute("DefaultApi", new { status = "error" }, new { status = "error:"+ex.ToString(), noti = nm, msg = ex.ToString() });
            }

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
                noti.notificacion_tipo = n.notificacion_tipo;
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