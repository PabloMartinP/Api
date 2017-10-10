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
    public class tecnicosController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        public Models.tecnicoModel buscarTecnico(string id)
        {
            Models.tecnicoModel tecnico = new Models.tecnicoModel();

            vw_calificacion_tecnico ct = db.vw_calificacion_tecnico.Where(x => x.id == id).FirstOrDefault();
            List<vw_ot_abiertas> ots = db.vw_ot_abiertas.Where(x => x.tecnico_sk == ct.sk).ToList();

            tecnico.id = ct.id;
            tecnico.psw = ct.psw;
            tecnico.nombre = ct.nombre;
            tecnico.sk = ct.sk;
            tecnico.email = ct.email;
            tecnico.calificacion = ct.calificacion;
            
            List<Models.tecnicoOtModel> otsTecnico = new List<Models.tecnicoOtModel>();

            foreach (vw_ot_abiertas o in ots)
            {
                otsTecnico.Add(new tecnicoOtModel
                {
                    ot_id = o.ot_id,
                    estado = o.estado,
                    estado_desc = o.estado_desc,
                    fecha = o.fecha
                });
            }

            tecnico.ots = otsTecnico;
            
            return tecnico;
        }

        // GET: api/tecnicos/5
        [ResponseType(typeof(tecnicoModel))]
        public IHttpActionResult Gettecnico(string id)
        {
            Models.tecnicoModel tecnico = buscarTecnico(id);
            if (tecnico == null)
            {
                return NotFound();
            }

            return Ok(tecnico);
        }
        /*
        
        // POST: api/tecnicos
        [ResponseType(typeof(tecnico))]
        public IHttpActionResult Posttecnico(tecnico tecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tecnicos.Add(tecnico);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tecnicoExists(tecnico.tecnico_sk))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tecnico.tecnico_sk }, tecnico);
        }
*/
    }
}