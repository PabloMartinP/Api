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
    public class notificaciones_zonaController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        private Service.FirebaseService fb = new Service.FirebaseService();
        private Service.ClienteService _clienteService = new Service.ClienteService();


        // POST: api/notificaciones_zona
        [ResponseType(typeof(Models.notificacionesZonaModel))]
        public IHttpActionResult Postbt_notificaciones(Models.notificacionesZonaModel n)
        {

            // Creo en el maestro la notificacion
            Data.lk_notificacion noti = new Data.lk_notificacion();
            noti.notificacion_desc = n.notificacion_desc;
            noti.notificacion_texto = n.notificacion_texto;
            noti.notificacion_tipo = n.notificacion_tipo;
            db.lk_notificacion.Add(noti);
            db.SaveChanges();

            // Busco todos los usuarios de la localidad
            List<Data.cliente> clientes = _clienteService.findClientsByLocalidad(n.localidad_sk);
            List<Data.usuario> usuarios = _clienteService.findUsersByClients(clientes);

            // Armo una notificacion por cada Usuario
            foreach (usuario u in usuarios)
            {
                
                Data.bt_notificaciones bt_not_aux = new Data.bt_notificaciones();
                bt_not_aux.usuario_sk = u.usuario_sk;
                bt_not_aux.cliente_sk = u.cliente_sk;
                bt_not_aux.notificacion_sk = noti.notificacion_sk;
                bt_not_aux.tiempo_sk = DateTime.Today;

                db.bt_notificaciones.Add(bt_not_aux);
                db.SaveChanges();
                    
                // Mando Notificacion Push
                Service.FirebaseService.notificacion_mensaje m = new Service.FirebaseService.notificacion_mensaje();
                m.usuario_sk = u.usuario_sk;
                m.cliente_sk = 0;
                m.titulo = noti.notificacion_desc;
                m.descripcion = noti.notificacion_texto;
                fb.EnviarAFCM(m);

            }

            return CreatedAtRoute("DefaultApi", new { id = n.notificacion_tipo }, n);
        }
    }
}