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
    public class ISP_AlertasPromController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ISP_AlertasProm/5
        [ResponseType(typeof(List<vw_notificaciones>))]
        public IHttpActionResult GetISP_AlertasProm(string id)
        {
            List<vw_notificaciones> listado = new List<vw_notificaciones>();

            listado = db.vw_notificaciones.Where(x => x.tipo == id).ToList();            

            if (listado == null)
            {
                return NotFound();
            }

            return Ok(listado);
        }
        
    }
}