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

namespace Netmefy.Api.Controllers.api
{
    public class ot_statusController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        private Service.OTService _otService = new Service.OTService();
        private Service.ClienteService _clienteService = new Service.ClienteService();
        private Service.FirebaseService fb = new Service.FirebaseService();


        // GET: api/ot_status/5
        [ResponseType(typeof(Models.ot_statusModel))]
        public IHttpActionResult Getbt_ot_status(int ot_id)
        {
            //List<bt_ot_status> estados = db.bt_ot_status.Where(x => x.ot_id == ot_id).ToList();
            //if (estados == null)
            //{
            //    return NotFound();
            //}

            //List<Models.ot_statusModel> modelEstados = Models.ot_statusModel.ListConvertTo(estados);
            //Models.ot_statusModel ult_estado = modelEstados.OrderByDescending(x => x.timestamp).FirstOrDefault();

            Data.bt_ot_status ult_estado = _otService.buscarUltEstado(ot_id);
            Models.ot_statusModel modelEstado = Models.ot_statusModel.ConvertTo(ult_estado);

            return Ok(modelEstado);
        }


        [ResponseType(typeof(Models.ot_statusModel))]
        public IHttpActionResult Postbt_ot_status(Models.ot_statusModel estado)
        {
            string notificacion_desc = "";
            string notificacion_texto = "";
            bt_ot_status bt_ot_status = Models.ot_statusModel.ConvertToBD(estado);
            db.bt_ot_status.Add(bt_ot_status);
            db.SaveChanges();

            estado.tiempo_sk = bt_ot_status.tiempo_sk.ToString("yyyy-MM-dd");
            estado.hh_mm_ss = bt_ot_status.hh_mm_ss;
            estado.timestamp = string.Concat(bt_ot_status.tiempo_sk.ToString("yyyy-MM-dd"), " ", bt_ot_status.hh_mm_ss);
            
            
            if(estado.estado_sk == 3 || estado.estado_sk == 2 )
            {
                // actualizo el estado de cierre de la OT
                Data.bt_ord_trabajo ot = db.bt_ord_trabajo.Where(x => x.ot_id == estado.ot_id).FirstOrDefault();
                
                if(estado.estado_sk == 3)
                {
                    //FINALIZADA
                    ot.fh_cierre = DateTime.Today;
                    db.SaveChanges();

                    notificacion_desc = string.Concat("Orden ", estado.ot_id.ToString(), " resuelta");
                    notificacion_texto = string.Concat("La orden ", estado.ot_id.ToString(), " ha sido resuelta, ante cualquier consulta no dude en informarnos");
                }
                else
                {
                    // EN CURSO
                    tecnico t = null;
                    int tecnico_sk;
                    if (ot.tecnico_sk != null) {
                        tecnico_sk = (int)ot.tecnico_sk;
                        t = (from tec in db.tecnicos
                                     where tec.tecnico_sk == tecnico_sk
                                     select tec).FirstOrDefault();
                    }
                        

                    

                    notificacion_desc = string.Concat("Orden ", estado.ot_id.ToString(), " en curso");
                    if(t != null)
                        notificacion_texto = "Técnico asignado: " + t.tecnico_desc;// string.Concat("El reclamo ", estado.ot_id.ToString(), " esta en curso");
                }

                // Doy de alta la notificacion en la LK
                Data.lk_notificacion noti = new Data.lk_notificacion();
                noti.notificacion_desc = notificacion_desc;
                noti.notificacion_texto = notificacion_texto;
                noti.notificacion_tipo = "OT y OS";
                db.lk_notificacion.Add(noti);
                db.SaveChanges();

                // Busco los usuarios del cliente de la OT
                List<usuario> usuarios = _clienteService.findUsersByClient(ot.cliente_sk);

                // Armo una notificacion por cada Usuario
                foreach (usuario u in usuarios)
                {
                    Data.bt_notificaciones bt_not = new Data.bt_notificaciones();
                    bt_not.usuario_sk = u.usuario_sk;
                    bt_not.cliente_sk = u.cliente_sk;
                    bt_not.notificacion_sk = noti.notificacion_sk;
                    bt_not.tiempo_sk = DateTime.Today;
                    bt_not.ot_id = estado.ot_id;

                    db.bt_notificaciones.Add(bt_not);
                }
                db.SaveChanges();

                // Mando Notificacion Push
                Service.FirebaseService.notificacion_mensaje m = new Service.FirebaseService.notificacion_mensaje();
                m.cliente_sk = ot.cliente_sk;
                m.usuario_sk = 0;
                m.titulo = noti.notificacion_desc;
                fb.EnviarAFCM(m);

            }

            

            return CreatedAtRoute("DefaultApi", new { id = estado.ot_id }, estado);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bt_ot_statusExists(int id)
        {
            return db.bt_ot_status.Count(e => e.ot_id == id) > 0;
        }
    }
}