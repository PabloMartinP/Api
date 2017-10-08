using Netmefy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Service
{
    public class OSService
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        public Data.lk_tipo_os[] buscarTipoSolicitudes()
        {
            var j = db.lk_tipo_os.OrderBy(x => x.tipo_os_sk).ToArray();
            return j;
        }

        public List<Data.bt_solicitudes> buscarOsXCliente(int cliente_sk)
        {
            var j = db.bt_solicitudes.Where(x => x.cliente_sk == cliente_sk).ToList();
            return j;
        }

        public Data.bt_os_status buscarUltEstado(int os_id)
        {
            bt_os_status estado = db.bt_os_status.Where(x => x.os_id == os_id).OrderByDescending(x=>x.tiempo_sk).ThenByDescending(x=> x.hh_mm_ss).FirstOrDefault();
            
            return estado;

        }

        public Data.lk_estado buscarEstado(int id)
        {
            lk_estado estado = db.lk_estado.Where(x => x.estado_sk == id).FirstOrDefault();

            return estado;

        }


    }
}