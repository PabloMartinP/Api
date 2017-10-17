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
    public class ISP_TecnicosController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        
        // GET: api/ISP_Tecnicos/5
        [ResponseType(typeof(Models.ISP_TecnicosModel))]
        public IHttpActionResult GetISP_Tecnicos()
        {
            List<vw_calificacion_tecnico> vw_tecnicos = db.vw_calificacion_tecnico.ToList();
            List<Models.ISP_TecnicosModel> tecnicos = new List<Models.ISP_TecnicosModel>();

            foreach(vw_calificacion_tecnico t in vw_tecnicos){
                Models.ISP_TecnicosModel tecnico = new Models.ISP_TecnicosModel();

                tecnico.id = t.id;
                tecnico.nombre = t.nombre;
                tecnico.mail = t.email;
                tecnico.calificacion = t.calificacion;
                tecnico.otAsignadas = t.ot_asignadas;
                tecnico.otCerradas = t.ot_cerradas;

                tecnicos.Add(tecnico);
            }
            
            return Ok(tecnicos);
        }
        
    }
}