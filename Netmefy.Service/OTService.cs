using Netmefy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Service
{
    public class OTService
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        public Data.lk_tipo_ot[] buscarTipoSolicitudes()
        {
            var j = db.lk_tipo_ot.OrderBy(x => x.tipo_ot_sk).ToArray();
            return j;
        }

        public List<Data.bt_ord_trabajo> buscarOtXCliente(int cliente_sk)
        {
            var j = db.bt_ord_trabajo.Where(x => x.cliente_sk == cliente_sk).ToList();
            return j;
        }

        public Data.bt_ord_trabajo buscarOtXOTID(int ot_id)
        {
            var j = db.bt_ord_trabajo.Where(x => x.ot_id == ot_id).FirstOrDefault();
            return j;
        }

        public Data.bt_ot_status buscarUltEstado(int ot_id)
        {
            bt_ot_status estado = db.bt_ot_status.Where(x => x.ot_id == ot_id).OrderByDescending(x=>x.tiempo_sk).ThenByDescending(x=> x.hh_mm_ss).FirstOrDefault();
            
            return estado;

        }

        public Data.lk_estado buscarEstado(int id)
        {
            lk_estado estado = db.lk_estado.Where(x => x.estado_sk == id).FirstOrDefault();

            return estado;

        }


    }
}