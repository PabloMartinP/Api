using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class os_statusModel
    {

        public int os_id { get; set; }
        public string tiempo_sk { get; set; }
        public string hh_mm_ss { get; set; }
        public int estado_sk { get; set; }
        public string comentarios { get; set; }
        public string timestamp { get; set; }

        public static List<os_statusModel> ListConvertTo(List<Data.bt_os_status> estados)
        {
            List<os_statusModel> list = new List<os_statusModel>();

            foreach (Data.bt_os_status n in estados)
            {
                list.Add(ConvertTo(n));
            }

            return list;
        }

        public static os_statusModel ConvertTo(Data.bt_os_status estado)
        {
            os_statusModel e = new os_statusModel();

            e.os_id = estado.os_id;
            e.tiempo_sk = estado.tiempo_sk.ToString("dd-MM-yyyy");
            e.hh_mm_ss = estado.hh_mm_ss;
            e.estado_sk = estado.estado_sk;
            e.comentarios = estado.comentarios;
            e.timestamp = string.Concat(estado.tiempo_sk.ToString("yyyy-dd-MM")," ", estado.hh_mm_ss);

            return e;
        }

        public static Data.bt_os_status ConvertToBD(os_statusModel estado)
        {
            Data.bt_os_status e = new Data.bt_os_status();

            e.os_id = estado.os_id;

            if (estado.tiempo_sk != null)
                e.tiempo_sk = DateTime.ParseExact(estado.tiempo_sk, "dd-MM-yyyy", null);
            else
                e.tiempo_sk = DateTime.Today;

            if (estado.hh_mm_ss != null)
                e.hh_mm_ss = estado.hh_mm_ss;
            else
                e.hh_mm_ss = DateTime.Now.ToString("HH:mm:ss");

            e.estado_sk = estado.estado_sk;
            e.comentarios = estado.comentarios;
            
            return e;
        }


    }
}