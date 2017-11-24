using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class testsModel
    {

        public int test_id { get; set; }
        public int cliente_sk { get; set; }
        public int? ot_id { get; set; }
        public string tiempo_sk { get; set; }
        public string comentario { get; set; }
        public decimal? vel_mb_medidos { get; set; }
        public decimal? potencia_recep { get; set; }
        public int? flag_cableado_nuevo { get; set; }
        public int? flag_modem_ok { get; set; }
        public decimal? ping { get; set; }

        public static List<testsModel> ListConvertTo(List<Data.bt_tests> tests)
        {
            List<testsModel> list = new List<testsModel>();

            foreach (Data.bt_tests t in tests)
            {
                list.Add(ConvertTo(t));
            }

            return list;
        }

        public static testsModel ConvertTo(Data.bt_tests test)
        {
            testsModel t = new testsModel();

            t.test_id = test.test_id;
            t.cliente_sk = test.cliente_sk;
            t.ot_id = test.ot_id;
            t.tiempo_sk = test.tiempo_sk.ToString("dd-MM-yyyy");
            t.comentario = test.comentario;

            t.vel_mb_medidos = test.vel_mb_medidos;
            t.potencia_recep = test.potencia_recep;
            t.flag_cableado_nuevo = test.flag_clabeado_nuevo;
            t.flag_modem_ok = test.flag_modem_ok;
            t.ping = test.ping;

            return t;
        }

        public static Data.bt_tests ConvertToBD(testsModel test)
        {
            Data.bt_tests t = new Data.bt_tests();

            //t.test_id = test.test_id;

            if(test.cliente_sk == -1)
                t.cliente_sk = test.cliente_sk;
            else
                t.cliente_sk = test.cliente_sk;

            if (test.ot_id == -1)
                t.ot_id = null;
            else
                t.ot_id = test.ot_id;

            if (test.tiempo_sk != null)
                t.tiempo_sk = DateTime.ParseExact(test.tiempo_sk, "yyyy-MM-dd", null);
            else
                t.tiempo_sk = DateTime.Today;
            t.comentario = test.comentario;
            t.vel_mb_medidos = test.vel_mb_medidos;
            t.potencia_recep = test.potencia_recep;
            t.flag_clabeado_nuevo = test.flag_cableado_nuevo;
            t.flag_modem_ok = test.flag_modem_ok;
            t.ping = test.ping;
            
            return t;
        }


    }
}