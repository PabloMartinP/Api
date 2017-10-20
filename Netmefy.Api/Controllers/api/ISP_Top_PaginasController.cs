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
    public class ISP_Top_PaginasController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

      
        // GET: api/ISP_Top_Paginas/5
        [ResponseType(typeof(List<vw_top_paginas_x_zona>))]
        public IHttpActionResult Getvw_top_paginas_x_zona(string id)
        {
            List<vw_top_paginas_x_zona> paginas = db.vw_top_paginas_x_zona.Where(x => x.zona == id).OrderByDescending(x => x.cant_total).Take(100).ToList();

            if (paginas == null)
            {
                return NotFound();
            }

            return Ok(paginas);
        }
   }
}