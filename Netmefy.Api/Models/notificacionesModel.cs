using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class notificacionesModel
    {
        public int usuario_sk;
        public int cliente_sk;
        public int notificacion_sk;
        public System.DateTime tiempo_sk;

        public static List<notificacionesModel> ConvertTo(List<Data.bt_notificaciones> notificaciones)
        {
            List<notificacionesModel> list = new List<notificacionesModel>();

            foreach (Data.bt_notificaciones n in notificaciones)
            {
                list.Add(new notificacionesModel
                {
                    usuario_sk = n.usuario_sk,
                    cliente_sk = n.cliente_sk,
                    notificacion_sk = n.notificacion_sk,
                    tiempo_sk = n.tiempo_sk,

                });
            }

            return list;
        }
    }
}