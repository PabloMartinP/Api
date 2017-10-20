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
    public class ISP_TopTecnicosController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ISP_TopTecnicos/5
        [ResponseType(typeof(List<vw_tecnico_x_zona>))]
        public IHttpActionResult Getvw_tecnico_x_zona(string id)
        {
            List<vw_tecnico_x_zona> tecnicos = db.vw_tecnico_x_zona.Where(x=>x.zona == id).OrderByDescending(x => x.calificacion).Take(10).ToList();

            if (tecnicos == null)
            {
                return NotFound();
            }

            return Ok(tecnicos);
        }
   }
}