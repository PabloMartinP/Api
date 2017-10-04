using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class otModel
    {

        public int ot_id { get; set; }
        public int cliente_sk { get; set; }
        public int tecnico_sk { get; set; }
        public string fh_creacion { get; set; }
        public string fh_cierre { get; set; }
        public int calificacion { get; set; }
        public string descripcion { get; set; }

        public static List<otModel> ListConvertTo(List<Data.bt_ord_trabajo> ots)
        {
            List<otModel> list = new List<otModel>();

            foreach (Data.bt_ord_trabajo n in ots)
            {
                list.Add(ConvertTo(n));
            }

            return list;
        }

        public static otModel ConvertTo(Data.bt_ord_trabajo n)
        {
            otModel ot = new otModel();

            DateTime fh_cierre_d;

            if (n.fh_cierre != null)
                fh_cierre_d = (DateTime)n.fh_cierre;
            else
                fh_cierre_d = DateTime.MaxValue;

            ot.ot_id = n.ot_id;
            ot.cliente_sk = n.cliente_sk;
            ot.tecnico_sk = (int)n.tecnico_sk;
            ot.fh_creacion = n.fh_creacion.ToString("dd-MM-yyyy");
            ot.fh_cierre = fh_cierre_d.ToString("dd-MM-yyyy");
            ot.calificacion = (int)n.calificacion;
            ot.descripcion = n.descripcion;

            return ot;
        }

        public static Data.bt_ord_trabajo ConvertToBD(otModel orden)
        {
            Data.bt_ord_trabajo ot = new Data.bt_ord_trabajo();
            
            ot.ot_id = orden.ot_id;
            ot.cliente_sk = orden.cliente_sk;
            ot.tecnico_sk = orden.tecnico_sk;
            ot.calificacion = orden.calificacion;
            ot.descripcion = orden.descripcion;

            if (orden.fh_creacion != null)
                ot.fh_creacion = DateTime.ParseExact(orden.fh_creacion, "dd-MM-yyyy", null);
            else
                ot.fh_creacion = DateTime.Today;
            

            if (orden.fh_cierre != null)
                ot.fh_cierre = DateTime.ParseExact(orden.fh_cierre, "dd-MM-yyyy", null);
            else
                ot.fh_cierre = DateTime.ParseExact("31-12-2999", "dd-MM-yyyy", null); ;
       

            return ot;
        }


    }
}