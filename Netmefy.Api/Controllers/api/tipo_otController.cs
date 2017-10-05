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
    public class tipo_otController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        
        // GET: api/tipo_ot/5
        [ResponseType(typeof(List<lk_tipo_ot>))]
        public IHttpActionResult Getlk_tipo_ot()
        {
            List<lk_tipo_ot> lk_tipo_ot = db.lk_tipo_ot.ToList();
            if (lk_tipo_ot == null)
            {
                return NotFound();
            }

            return Ok(lk_tipo_ot);
        }

    }
}