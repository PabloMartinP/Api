using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class solicitudesModel
    {

        public int os_id;
        public int cliente_sk;
        public string fh_creacion;
        public string fh_cierre;

        public static List<solicitudesModel> ListConvertTo(List<Data.bt_solicitudes> solicitudes)
        {
            List<solicitudesModel> list = new List<solicitudesModel>();

            foreach (Data.bt_solicitudes n in solicitudes)
            {
                DateTime fh_creacion_d = (DateTime)n.fh_creacion;
                DateTime fh_cierre_d = (DateTime)n.fh_cierre;

                list.Add(new solicitudesModel
                {
                    os_id = n.os_id,
                    cliente_sk = n.cliente_sk,
                    fh_creacion = fh_creacion_d.ToString("dd-MM-yyyy"),
                    fh_cierre = fh_cierre_d.ToString("dd-MM-yyyy"),
                });
            }

            return list;
        }

        public static solicitudesModel ConvertTo(Data.bt_solicitudes solicitudes)
        {
            solicitudesModel solpe = new solicitudesModel();
            
            DateTime fh_creacion_d = (DateTime)solicitudes.fh_creacion;
            DateTime fh_cierre_d = (DateTime)solicitudes.fh_cierre;

            solpe.os_id = solicitudes.os_id;
            solpe.cliente_sk = solicitudes.cliente_sk;
            solpe.fh_creacion = fh_creacion_d.ToString("dd-MM-yyyy");
            solpe.fh_cierre = fh_cierre_d.ToString("dd-MM-yyyy");

            return solpe;
        }

        public static Data.bt_solicitudes ConvertToBD(solicitudesModel solicitudes)
        {
            Data.bt_solicitudes solpe = new Data.bt_solicitudes();
            
            solpe.os_id = solicitudes.os_id;
            solpe.cliente_sk = solicitudes.cliente_sk;
            solpe.fh_creacion = DateTime.ParseExact(solicitudes.fh_creacion,"dd-MM-yyyy",null);
            solpe.fh_cierre = DateTime.ParseExact(solicitudes.fh_cierre, "dd-MM-yyyy", null); ;

            return solpe;
        }


    }
}