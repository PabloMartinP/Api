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
    public class ISP_Velocidades_ContratadasController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ISP_Velocidades_Contratadas/5
        [ResponseType(typeof(List<Models.ISP_VelocidadesContratadas>))]
        public IHttpActionResult GetVelocidades_Contratadas(string id)
        {
            List<vw_porc_vel> velocidades = db.vw_porc_vel.Where(x => x.zona == id).ToList();
            if (velocidades == null)
            {
                return NotFound();
            }

            List<Models.ISP_VelocidadesContratadas> vel = new List<Models.ISP_VelocidadesContratadas>();

            foreach (vw_porc_vel v in velocidades)
            {
                Models.ISP_VelocidadesContratadas vel_aux = new Models.ISP_VelocidadesContratadas();

                vel_aux.zona = v.zona;
                vel_aux.nombre = v.velocidad;
                vel_aux.valor = v.porc;

                vel.Add(vel_aux);
            }

            return Ok(vel);
        }
        
    }
}