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
    public class ISP_Top_ClientesController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ISP_Top_Clientes/5
        [ResponseType(typeof(List<vw_top_clientes_ot>))]
        public IHttpActionResult Getvw_top_clientes_ot(string id)
        {
            List<vw_top_clientes_ot> clientes = new List<vw_top_clientes_ot>();

            if (id == "ARGENTINA")
            {
                clientes = db.vw_top_clientes_ot.OrderByDescending(x => x.cant_reclamos).Take(20).ToList();
            }
            else
            { 
                clientes = db.vw_top_clientes_ot.Where(x => x.zona == id).OrderByDescending(x => x.cant_reclamos).Take(20).ToList();
            }

            if (clientes == null)
            {
                return NotFound();
            }

            return Ok(clientes);
        }
   }
}