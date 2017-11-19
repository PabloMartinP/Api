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
using Netmefy.Service;
using Netmefy.Api.Models;

namespace Netmefy.Api.Controllers.api
{
    public class ISP_clientesController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ISP_clientes/5
        [ResponseType(typeof(clientIDModel))]
        public IHttpActionResult Getcliente()
        {

            List<Data.cliente> clientes_db = db.clientes.ToList();
            List<clientIDModel> clientes = new List<clientIDModel>();

            if (clientes_db == null)
            {
                return NotFound();
            }

            foreach (Data.cliente c in clientes_db)
            {
                clientIDModel cli = new clientIDModel();

                cli.id = c.cliente_id;
                cli.sk = c.cliente_sk;
                cli.username = c.cliente_desc;

                clientes.Add(cli);

            }

            return Ok(clientes);
        }
    }
}