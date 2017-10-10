using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Netmefy.Data;

namespace Netmefy.Api.Controllers
{
    public class solicitudesController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        private Service.OSService _osService = new Service.OSService();


        // GET: api/solicitudes/5
        [ResponseType(typeof(Models.solicitudesModel))]
        public IHttpActionResult Getbt_solicitudes(int cliente_sk)
        {
            List<bt_solicitudes> solicitudes = _osService.buscarOsXCliente(cliente_sk);
            Data.lk_tipo_os[] tipos = _osService.buscarTipoSolicitudes();

            List<Models.solicitudesModel> solpe = Models.solicitudesModel.ListConvertTo(solicitudes,tipos);

            foreach (Models.solicitudesModel os in solpe)
            {
                Data.bt_os_status os_st = _osService.buscarUltEstado(os.os_id);
                os.estado_id = os_st.estado_sk;
                os.estado_desc = _osService.buscarEstado(os_st.estado_sk).estado_desc;
            }


            return Ok(solpe);
        }

        // POST: api/solicitudes
        [ResponseType(typeof(Models.solicitudesModel))]        
        //public IHttpActionResult Postbt_solicitudes(Models.solicitudesModel solicitud)
        public IHttpActionResult Postbt_solicitudes(Models.solicitudesModel solicitud)
        {
            
            bt_solicitudes bt_solicitudes = Models.solicitudesModel.ConvertToBD(solicitud);

            if (bt_solicitudes.os_id == 0)
            {
                db.bt_solicitudes.Add(bt_solicitudes);

                db.SaveChanges();
                solicitud.fh_cierre = ((DateTime)(bt_solicitudes.fh_cierre)).ToString("yyyy-MM-dd"); ;
                solicitud.fh_creacion = ((DateTime)(bt_solicitudes.fh_creacion)).ToString("yyyy-MM-dd"); ;
                solicitud.os_id = bt_solicitudes.os_id;
                solicitud.estado_id = 1;
                solicitud.estado_desc = _osService.buscarEstado(1).estado_desc;

                // Creo la el estado 1 para la orden
                Models.os_statusModel st = new Models.os_statusModel();
                st.estado_sk = 1;
                st.os_id = bt_solicitudes.os_id;
                st.comentarios = "Nueva Orden";
                bt_os_status bt_os_status = Models.os_statusModel.ConvertToBD(st);


                db.bt_os_status.Add(bt_os_status);
                db.SaveChanges();


                return CreatedAtRoute("DefaultApi", new { id = bt_solicitudes.os_id }, solicitud);

            }
            else
            {
                bt_solicitudes solpe = db.bt_solicitudes.Where(x => x.os_id == bt_solicitudes.os_id).FirstOrDefault();

                solpe.fh_creacion = bt_solicitudes.fh_creacion;
                solpe.fh_cierre = bt_solicitudes.fh_cierre;
                solpe.tipo = bt_solicitudes.tipo;
                solpe.descripcion = bt_solicitudes.descripcion;

                db.SaveChanges();
                solicitud.fh_creacion = ((DateTime)(solpe.fh_creacion)).ToString("yyyy-MM-dd"); ;
                solicitud.fh_cierre = ((DateTime)(solpe.fh_cierre)).ToString("yyyy-MM-dd");
                solicitud.estado_id = _osService.buscarUltEstado(solicitud.os_id).estado_sk;
                solicitud.estado_desc = _osService.buscarEstado(solicitud.estado_id).estado_desc;


                return CreatedAtRoute("DefaultApi", new { id = solpe.os_id }, solicitud);
            }

        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bt_solicitudesExists(int id)
        {
            return db.bt_solicitudes.Count(e => e.os_id == id) > 0;
        }
    }
}