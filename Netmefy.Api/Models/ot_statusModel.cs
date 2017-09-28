using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class ot_statusModel
    {

        public int ot_id { get; set; }
        public string tiempo_sk { get; set; }
        public string hh_mm_ss { get; set; }
        public int estado_sk { get; set; }
        public string comentarios { get; set; }
        public string timestamp { get; set; }

        public static List<ot_statusModel> ListConvertTo(List<Data.bt_ot_status> estados)
        {
            List<ot_statusModel> list = new List<ot_statusModel>();

            foreach (Data.bt_ot_status n in estados)
            {
                list.Add(ConvertTo(n));
            }

            return list;
        }

        public static ot_statusModel ConvertTo(Data.bt_ot_status estado)
        {
            ot_statusModel e = new ot_statusModel();

            e.ot_id = estado.ot_id;
            e.tiempo_sk = estado.tiempo_sk.ToString("dd-MM-yyyy");
            e.hh_mm_ss = estado.hh_mm_ss;
            e.estado_sk = estado.estado_sk;
            e.comentarios = estado.comentarios;
            e.timestamp = string.Concat(estado.tiempo_sk.ToString("yyyy-dd-MM")," ", estado.hh_mm_ss);

            return e;
        }

        public static Data.bt_ot_status ConvertToBD(ot_statusModel estado)
        {
            Data.bt_ot_status e = new Data.bt_ot_status();

            e.ot_id = estado.ot_id;

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