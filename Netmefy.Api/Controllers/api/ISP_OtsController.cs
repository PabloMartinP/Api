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
    public class ISP_OtsController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ISP_Ots/5
        [ResponseType(typeof(List<vw_isp_ots>))]
        public IHttpActionResult Getvw_isp_ots()
        {
            List<vw_isp_ots> vw_isp_ots = db.vw_isp_ots.ToList();
            if (vw_isp_ots == null)
            {
                return NotFound();
            }

            return Ok(vw_isp_ots);
        }

   }
}