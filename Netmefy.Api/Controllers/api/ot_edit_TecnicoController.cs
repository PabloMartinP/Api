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
    public class ot_edit_TecnicoController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        private Service.OTService _otService = new Service.OTService();
        private Service.ClienteService _clienteService = new Service.ClienteService();
        private Service.FirebaseService fb = new Service.FirebaseService();


        // POST: api/ot_edit_Tecnico
        [ResponseType(typeof(Models.ot_edit_TecnicoModel))]        
        public IHttpActionResult Postbt_ord_trabajo(Models.ot_edit_TecnicoModel orden)
        {
            bt_ord_trabajo ot = db.bt_ord_trabajo.Where(x=> x.ot_id == orden.ot_id).FirstOrDefault();

            if(ot == null)
            {
                return NotFound();
            }

            ot.tecnico_sk = orden.tecnico_sk;
            db.SaveChanges();

            return Ok(orden);
        }
    }
}