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
        public string tiempo_sk { get; set; }
        public string notificacion_desc { get; set; }
        public string notificacion_texto { get; set; }
        public int? ot_id { get; set; }
        public decimal ot_calificacion { get; set; }

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
                    tiempo_sk = n.tiempo_sk.ToString("yyyy-MM-dd"),
                    ot_id = n.ot_id,
                    notificacion_desc= ns.buscarNotificaciones(n.notificacion_sk).notificacion_desc,
                    notificacion_texto = ns.buscarNotificaciones(n.notificacion_sk).notificacion_texto,

                });
            }

            return list;
        }

        public static Data.bt_notificaciones ConvertToBD(Models.notificacionesModel n)
        {
            Data.bt_notificaciones not = new Data.bt_notificaciones();


            not.usuario_sk = n.usuario_sk;
            not.cliente_sk = n.cliente_sk;
            not.notificacion_sk = n.notificacion_sk;
            not.tiempo_sk = DateTime.Today;
            not.ot_id = n.ot_id;
             
            return not;
        }
    }
}