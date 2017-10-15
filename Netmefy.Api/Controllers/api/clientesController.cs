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
    public class clientesController : ApiController
    {
        //private NETMEFYEntities db = new NETMEFYEntities();
        private ClienteService _clientService = new ClienteService();
        
        // GET: api/clientes/5
        [ResponseType(typeof(clientInfoModel))]
        public IHttpActionResult Getcliente(string username)
        {

            cliente cliente = _clientService.buscar(username);
            
            if (cliente == null)
            {
                return NotFound();
            }

            router router = _clientService.getRouterFromClient(cliente.cliente_sk);
            List<dispositivo> devices = _clientService.getDevices(cliente.cliente_sk, router.router_sk);
            List<lk_web> webs = router.lk_web.ToList();
            List<webModel> websModel = new List<webModel>();
            foreach (var w in webs)     
            {
                websModel.Add(new webModel
                {
                    ip = w.web_ip, url = w.web_url, id = w.web_sk, nombre = w.web_nombre
                });
            }

            List<dispositivoInfoModel> devicesModel = dispositivoInfoModel.ConvertTo(devices);


            clientInfoModel info = new clientInfoModel
            {
                id = cliente.cliente_sk,
                username = cliente.cliente_id,
                mb_contratado = cliente.cliente_vel_mb_contr,
                mb_umbral = cliente.cliente_vel_mb_umbral,
                nombre = cliente.cliente_desc,
                router = new routerInfoModel
                {
                    router_sk = router.router_sk, 
                    modelo = router.router_modelo,
                    ssid = router.router_ssid,
                    password = router.router_psw,
                    devices = devicesModel, 
                    webs_bloqueadas = websModel
                }
            };




            return Ok(info);
        }
        /*
        // GET: api/clientes
        public IQueryable<cliente> Getclientes()
        {
            return db.clientes;
        }


        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcliente(int id, cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.cliente_sk)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/clientes
        [ResponseType(typeof(cliente))]
        public IHttpActionResult Postcliente(cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.clientes.Add(cliente);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (clienteExists(cliente.cliente_sk))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.cliente_sk }, cliente);
        }

        // DELETE: api/clientes/5
        [ResponseType(typeof(cliente))]
        public IHttpActionResult Deletecliente(int id)
        {
            cliente cliente = db.clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.clientes.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clienteExists(int id)
        {
            return db.clientes.Count(e => e.cliente_sk == id) > 0;
        }*/
    }
}