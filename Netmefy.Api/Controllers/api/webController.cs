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
    public class webController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();


        // GET: api/usuarios/5
        [ResponseType(typeof(List<Models.webModel>))]
        public IHttpActionResult Getweb()
        {
            List<Data.lk_web> webs = db.lk_web.ToList();

            List<Models.webModel> websModel = new List<Models.webModel>();
            foreach (var w in webs)
            {
                websModel.Add(new Models.webModel
                {
                    ip = w.web_ip,
                    url = w.web_url,
                    id = w.web_sk,
                    nombre = w.web_nombre, 
                    resid_imagen = w.web_imagen
                });
            }
            /*
            if (websModel == null)
            {
                return NotFound();
            }*/

            return Ok(websModel);
        }
        // DELETE: api/usuarios/5
        [HttpDelete]
        [ResponseType(typeof(lk_web))]
        public IHttpActionResult Deleteweb(int id)
        {
            //DEVUELVE -1 SI NO EXISTE, -1 SI HAY UN ERROR(YA ESTA USADA)
            try
            {
                
                lk_web web = db.lk_web.Find(id);
                if (web == null)
                {

                    
                    lk_web web2 = new lk_web();
                    web2.web_sk = -2;
                    return Ok(web2);
                }

                db.lk_web.Remove(web);
                db.SaveChanges();

                return Ok(web);
            }
            catch (Exception ex)
            {
                lk_web web = new lk_web();
                web.web_sk = -1;
                return Ok(web);
            }
        }



        // POST: api/web
        [ResponseType(typeof(Models.webModel))]
        public IHttpActionResult Postlk_web(Models.webModel modelWeb )
        {


            Data.lk_web web = new Data.lk_web
            {
                web_sk = modelWeb.id,
                web_url = modelWeb.url,
                web_ip = modelWeb.ip,
                web_nombre = modelWeb.nombre,
                web_imagen = modelWeb.resid_imagen

            };

            if (web.web_sk == 0)
            {
                db.lk_web.Add(web);

                db.SaveChanges();

                modelWeb.id = web.web_sk;

                return CreatedAtRoute("DefaultApi", new { id = modelWeb.id }, modelWeb);

            } else
            {
                lk_web webAux = db.lk_web.Where(x => x.web_sk == web.web_sk).FirstOrDefault();
                webAux.web_nombre = web.web_nombre;
                webAux.web_nombre = web.web_nombre;
                webAux.web_ip = web.web_ip;
                webAux.web_url = web.web_url;

                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = modelWeb.id }, modelWeb);
            }


        }
        
    }
}