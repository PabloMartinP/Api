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
    public class otController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        private Service.OTService _otService = new Service.OTService();
        private Service.ClienteService _clienteService = new Service.ClienteService();
        private Service.FirebaseService fb = new Service.FirebaseService();

        // GET: api/ordenes/5
        [ResponseType(typeof(Models.otModel))]
        public IHttpActionResult Getbt_ord_trabajo(int cliente_sk)
        {
            List<bt_ord_trabajo> ots = _otService.buscarOtXCliente(cliente_sk);
            Data.lk_tipo_ot[] tipos = _otService.buscarTipoSolicitudes();

            List<Models.otModel> ordenes = Models.otModel.ListConvertTo(ots,tipos);

            foreach (Models.otModel ot in ordenes)
            {
                Data.bt_ot_status ot_st = _otService.buscarUltEstado(ot.ot_id);
                ot.estado_id = ot_st.estado_sk;
                ot.estado_desc = _otService.buscarEstado(ot_st.estado_sk).estado_desc;
            }

            return Ok(ordenes);
        }

        // POST: api/ordenes
        [ResponseType(typeof(Models.otModel))]        
        public IHttpActionResult Postbt_ord_trabajo(Models.otModel orden)
        {
            bt_ord_trabajo bt_ord_trabajo = Models.otModel.ConvertToBD(orden);

            if (bt_ord_trabajo.ot_id == 0)
            {
                // Creo la la orden
                db.bt_ord_trabajo.Add(bt_ord_trabajo);

                db.SaveChanges();
                orden.fh_cierre = ((DateTime)(bt_ord_trabajo.fh_cierre)).ToString("yyyy-mm-dd"); ;
                orden.fh_creacion = ((DateTime)(bt_ord_trabajo.fh_creacion)).ToString("yyyy-mm-dd"); ;
                orden.ot_id = bt_ord_trabajo.ot_id;
                orden.estado_id = 1;
                orden.estado_desc = _otService.buscarEstado(1).estado_desc;

                // Creo el estado 1 para la orden
                Models.ot_statusModel st = new Models.ot_statusModel();
                st.estado_sk = 1;
                st.ot_id = bt_ord_trabajo.ot_id;
                st.comentarios = "Nueva Orden";
                bt_ot_status bt_ot_status = Models.ot_statusModel.ConvertToBD(st);


                db.bt_ot_status.Add(bt_ot_status);
                db.SaveChanges();


                // Chequeo si es un reclamo y la cantidad por zona, y dependiendo eso genero las notificaciones
                if(orden.tipo_id != 1)
                {
                    Data.cliente cli = _clienteService.findClientBySK(orden.cliente_sk);
                    Data.vw_ot_porc_quejas porc_quejas = db.vw_ot_porc_quejas.Where(x => x.localidad_sk == cli.localidad_sk).FirstOrDefault();

                    if (porc_quejas != null)
                    {
                        if (porc_quejas.porc >= 20)
                        {
                            List<Data.cliente> clientes = _clienteService.findClientsByLocalidad(cli.localidad_sk);
                            List<Data.usuario> usuarios = _clienteService.findUsersByClients(clientes);
                            Data.lk_localidad loc = db.lk_localidad.Where(x => x.localidad_sk == cli.localidad_sk).FirstOrDefault();


                            // Busco si esta la Noti y sino la doy de alta la notificacion en la LK

                            Data.lk_notificacion noti = db.lk_notificacion.Where(x => x.notificacion_desc == String.Concat("Servicio con Inconvenientes - ", loc.localidad_desc)).FirstOrDefault();
                            if (noti == null)
                            {
                                Data.lk_notificacion noti_aux = new Data.lk_notificacion();
                                noti_aux.notificacion_desc = String.Concat("Servicio con Inconvenientes - ", loc.localidad_desc);
                                noti_aux.notificacion_texto = "El servicio presenta momentaneamente inconvenientes, estamos solucionandolo para su tranquilidad. Sepa disculpar las molestias";
                                noti_aux.notificacion_tipo = "Alerta";
                                db.lk_notificacion.Add(noti_aux);
                                db.SaveChanges();
                                noti = noti_aux;
                            }

                            // Armo una notificacion por cada Usuario
                            foreach (usuario u in usuarios)
                            {
                                Data.bt_notificaciones bt_not = db.bt_notificaciones.Where(x => x.usuario_sk == u.usuario_sk && x.notificacion_sk == noti.notificacion_sk && x.tiempo_sk == DateTime.Today).FirstOrDefault();

                                if (bt_not == null)
                                {
                                    Data.bt_notificaciones bt_not_aux = new Data.bt_notificaciones();
                                    bt_not_aux.usuario_sk = u.usuario_sk;
                                    bt_not_aux.cliente_sk = u.cliente_sk;
                                    bt_not_aux.notificacion_sk = noti.notificacion_sk;
                                    bt_not_aux.tiempo_sk = DateTime.Today;

                                    db.bt_notificaciones.Add(bt_not_aux);
                                    db.SaveChanges();
                                    bt_not = bt_not_aux;

                                    // Mando Notificacion Push
                                    Service.FirebaseService.notificacion_mensaje m = new Service.FirebaseService.notificacion_mensaje();
                                    m.usuario_sk = u.usuario_sk;
                                    m.cliente_sk = 0;
                                    m.titulo = noti.notificacion_desc;
                                    m.descripcion = "Estamos trabajando para solucionarlo, disculpe las molestias";
                                    fb.EnviarAFCM(m);

                                }



                            }
                        }
                    }
                }



                return CreatedAtRoute("DefaultApi", new { id = bt_ord_trabajo.ot_id }, orden);

            }
            else
            {
                bt_ord_trabajo ord_trabajo = db.bt_ord_trabajo.Where(x => x.ot_id == bt_ord_trabajo.ot_id).FirstOrDefault();

                ord_trabajo.fh_creacion = bt_ord_trabajo.fh_creacion;
                ord_trabajo.fh_cierre = bt_ord_trabajo.fh_cierre;
                ord_trabajo.calificacion = bt_ord_trabajo.calificacion;
                ord_trabajo.tecnico_sk = bt_ord_trabajo.tecnico_sk;
                ord_trabajo.tipo = bt_ord_trabajo.tipo;
                ord_trabajo.descripcion = bt_ord_trabajo.descripcion;

                db.SaveChanges();
                orden.fh_creacion = ((DateTime)(ord_trabajo.fh_creacion)).ToString("yyyy-mm-dd"); ;
                orden.fh_cierre = ((DateTime)(ord_trabajo.fh_cierre)).ToString("yyyy-mm-dd"); ;
                orden.estado_id = _otService.buscarUltEstado(orden.ot_id).estado_sk;
                orden.estado_desc = _otService.buscarEstado(orden.estado_id).estado_desc;

                return CreatedAtRoute("DefaultApi", new { id = ord_trabajo.ot_id }, orden);
            }

        }
        // PUT: api/paginas/5
        //[ResponseType(typeof(string))]
        [HttpPut]
        public IHttpActionResult updatearCalificacion(int id, int calificacion)
        {

            try
            {
                bt_ord_trabajo orden = db.bt_ord_trabajo.Where(x => x.ot_id == id).FirstOrDefault();
                orden.calificacion = calificacion;

                db.SaveChanges();

                Data.lk_tipo_ot[] tipos = _otService.buscarTipoSolicitudes();

                Models.otModel bt_ord_trabajo = Models.otModel.ConvertTo(orden, tipos);
                //return StatusCode(HttpStatusCode.NoContent);
                return CreatedAtRoute("DefaultApi", new { status = "ok" }, new { ot = bt_ord_trabajo });
            }
            catch (Exception ex)
            {
                return CreatedAtRoute("DefaultApi", new { status = "error" }, new { mensaje=  ex.ToString() });
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