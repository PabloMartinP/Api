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
    public class ISP_Zonas_ProblemasController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ISP_Zonas_Problemas/5
        [ResponseType(typeof(List<vw_alertas>))]
        public IHttpActionResult Getvw_alertas()
        {
            List<vw_alertas> vw_alertas = db.vw_alertas.ToList();
            if (vw_alertas == null)
            {
                return NotFound();
            }

            return Ok(vw_alertas);
        }

    }
}