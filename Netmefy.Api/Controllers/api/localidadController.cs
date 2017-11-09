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
    public class localidadController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();


        // GET: api/localidad/5
        [ResponseType(typeof(List<Models.localidadModel>))]
        public IHttpActionResult Getlk_localidad()
        {
            List<lk_localidad> locs = db.lk_localidad.ToList();
            if (locs == null)
            {
                return NotFound();
            }

            List<Models.localidadModel> list = Models.localidadModel.ListConvertTo(locs);

            return Ok(list);
        }
        
    }
}