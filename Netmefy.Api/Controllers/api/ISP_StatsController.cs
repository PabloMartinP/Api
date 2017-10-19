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
    public class ISP_StatsController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

    
        // GET: api/ISP_Stats/5
        [ResponseType(typeof(vw_stats_x_zona))]
        public IHttpActionResult GetISP_Stats(string id)
        {
            vw_stats_x_zona vw_stats_x_zona = db.vw_stats_x_zona.Where(x => x.zona == id).FirstOrDefault();
            if (vw_stats_x_zona == null)
            {
                return NotFound();
            }

            return Ok(vw_stats_x_zona);
        }
        
    }
}