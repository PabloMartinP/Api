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
    public class ISP_OTs_x_DiaController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/ISP_OTs_x_Dia/5
        [ResponseType(typeof(Models.ISP_OTs_x_Dia))]
        public IHttpActionResult GetISP_OTs_x_Dia(string id)
        {
            List<vw_ot_x_fh_creacion> ots = db.vw_ot_x_fh_creacion.Where(x => x.zona == id).ToList();
 
            if (ots == null)
            {
                return NotFound();
            }

            Models.ISP_OTs_x_Dia ots_x_dia = new Models.ISP_OTs_x_Dia();
            List<int> valores = new List<int>();

            ots_x_dia.zona = ots.FirstOrDefault().zona;
            ots_x_dia.startDate = ots.OrderByDescending(x => x.fecha).FirstOrDefault().fecha;

            foreach(vw_ot_x_fh_creacion cant in ots)
            {
                valores.Add((int)cant.cant_ot);
            }

            ots_x_dia.otsPorDia = valores;

            return Ok(ots_x_dia);
        }
   }
}