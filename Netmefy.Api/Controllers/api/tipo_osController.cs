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
    public class tipo_osController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/tipo_os/5
        [ResponseType(typeof(List<lk_tipo_os>))]
        public IHttpActionResult Getlk_tipo_os()
        {
            List<lk_tipo_os> lista_tipo_os = db.lk_tipo_os.ToList();
            if (lista_tipo_os == null)
            {
                return NotFound();
            }

            return Ok(lista_tipo_os);
        }
        
    }
}