using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class solicitudesModel
    {
        
        public int os_id { get; set; }
        public int cliente_sk { get; set; }
        public string fh_creacion { get; set; }
        public string fh_cierre { get; set; }
        public string descripcion { get; set; }
        public int tipo_id { get; set; }
        public string tipo { get; set; }

        public static List<solicitudesModel> ListConvertTo(List<Data.bt_solicitudes> solicitudes, Data.lk_tipo_os[] tipos)
        {
            List<solicitudesModel> list = new List<solicitudesModel>();

            foreach (Data.bt_solicitudes s in solicitudes)
            {
                Models.solicitudesModel sol = ConvertTo(s,tipos);
                list.Add(sol);
            }
            return list;
        }

        public static solicitudesModel ConvertTo(Data.bt_solicitudes solicitudes,Data.lk_tipo_os[] tipos)
        {
            solicitudesModel solpe = new solicitudesModel();

            DateTime fh_cierre_d;

            if (solicitudes.fh_cierre != null)
                fh_cierre_d = (DateTime)solicitudes.fh_cierre;
            else
                fh_cierre_d = DateTime.MaxValue;

            solpe.os_id = solicitudes.os_id;
            solpe.cliente_sk = solicitudes.cliente_sk;
            solpe.fh_creacion = solicitudes.fh_creacion.ToString("dd-MM-yyyy");
            solpe.fh_cierre = fh_cierre_d.ToString("dd-MM-yyyy");
            solpe.descripcion = solicitudes.descripcion;
            solpe.tipo_id = solicitudes.tipo;
            solpe.tipo = tipos[solicitudes.tipo-1].tipo_os_desc;

            return solpe;
        }

        public static Data.bt_solicitudes ConvertToBD(solicitudesModel solicitudes)
        {
            Data.bt_solicitudes solpe = new Data.bt_solicitudes();
            
            solpe.os_id = solicitudes.os_id;
            solpe.cliente_sk = solicitudes.cliente_sk;
            solpe.descripcion = solicitudes.descripcion;
            solpe.tipo = solicitudes.tipo_id;

            if (solicitudes.fh_creacion != null)
                solpe.fh_creacion = DateTime.ParseExact(solicitudes.fh_creacion, "dd-MM-yyyy", null);
            else
                solpe.fh_creacion = DateTime.Today;
            

            if (solicitudes.fh_cierre != null)
                solpe.fh_cierre = DateTime.ParseExact(solicitudes.fh_cierre, "dd-MM-yyyy", null);
            else
                solpe.fh_cierre = DateTime.ParseExact("31-12-2999", "dd-MM-yyyy", null); ;
       

            return solpe;
        }


    }
}