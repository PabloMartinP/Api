using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class notificacionesModel
    {
        public int usuario_sk { get; set; }
        public int cliente_sk { get; set; }
        public int notificacion_sk { get; set; }
        public System.DateTime tiempo_sk { get; set; }
        public string notificacion_desc { get; set; }
        public string notificacion_texto { get; set; }

        public static List<notificacionesModel> ConvertTo(List<Data.bt_notificaciones> notificaciones)
        {
            List<notificacionesModel> list = new List<notificacionesModel>();
            Service.NotificacionesService ns = new Service.NotificacionesService();


            foreach (Data.bt_notificaciones n in notificaciones)
            {
                list.Add(new notificacionesModel
                {
                    usuario_sk = n.usuario_sk,
                    cliente_sk = n.cliente_sk,
                    notificacion_sk = n.notificacion_sk,
                    tiempo_sk = n.tiempo_sk,
                    notificacion_desc= ns.buscarNotificaciones(n.notificacion_sk).notificacion_desc,
                    notificacion_texto = ns.buscarNotificaciones(n.notificacion_sk).notificacion_texto,

                });
            }

            return list;
        }
    }
}