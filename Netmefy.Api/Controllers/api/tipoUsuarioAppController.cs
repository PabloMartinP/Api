using Netmefy.Api.Models;
using Netmefy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Netmefy.Api.Controllers.api
{
    public class tipoUsuarioAppController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(tipoClienteModel))]
        public IHttpActionResult GettipoUsuario(string username)
        {
            LoginService ls = new LoginService();
            var tipoUsuario = ls.findByUsername(username);
            string tipo;

            if (tipoUsuario.tipo.ToLower().StartsWith("c"))
                tipo = "c";
            else
                tipo = "t";

            tipoClienteModel m = new tipoClienteModel
            {
                id = tipoUsuario.SK,
                tipo = tipo, 
                username = tipoUsuario.ID

            };
            return Ok(m);
        }
    }
}
