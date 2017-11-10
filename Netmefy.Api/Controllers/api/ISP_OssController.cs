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
    public class ISP_OssController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();
               
        // GET: api/ISP_Oss/5
        [ResponseType(typeof(List<vw_isp_oss>))]
        public IHttpActionResult Getvw_isp_oss()
        {
            List<vw_isp_oss> vw_isp_oss = db.vw_isp_oss.ToList();
            if (vw_isp_oss == null)
            {
                return NotFound();
            }

            return Ok(vw_isp_oss);
        }
        
    }
}