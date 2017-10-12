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

namespace Netmefy.Api.Controllers.api
{
    public class usuariosController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        private ClienteService _clientService = new ClienteService();


        //// GET: api/usuarios
        //public IQueryable<usuario> Getusuarios()
        //{
        //    return db.usuarios;
        //}
        
        

        // GET: api/usuarios/5
        [ResponseType(typeof(Models.usuarioModel))]
        public IHttpActionResult Getusuario(int id)
        {
            Data.usuario data_usr = _clientService.findUserById(id);
            List<pagina> paginas = data_usr.paginas.ToList();

            Models.usuarioModel usuario = Models.usuarioModel.ConvertTo(data_usr);
            
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.paginas = paginas.Select(x => x.entidad_desc).ToList();
            return Ok(usuario);
        }

        //// PUT: api/usuarios/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putusuario(int id, usuario usuario)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != usuario.usuario_sk)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(usuario).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!usuarioExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/usuarios
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Postusuario(usuario usuario)
        {
            try
            {




                usuario.usuario_sk = _clientService.guardarSiEsQueNoExiste(usuario);

                //return CreatedAtRoute("DefaultApi", new { id = usuario.usuario_sk }, usuario);
                return CreatedAtRoute("DefaultApi", new { id = usuario.usuario_sk }, new { status="ok", usuario= usuario});

            }
            catch (Exception ex)
            {
                usuario.usuario_sk = -1;
                usuario.usuario_email = "error:" + ex.ToString();
                return CreatedAtRoute("DefaultApi", new { id = -1 }, new { status = ex.ToString(), usuario = usuario });
            }
            
        }

        /*
        // DELETE: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Deleteusuario(int id)
        {
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usuarioExists(int id)
        {
            return db.usuarios.Count(e => e.usuario_sk == id) > 0;
        }*/
    }
}