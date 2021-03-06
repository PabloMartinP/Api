﻿using System;
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
using System.Web.Script.Serialization;
using Netmefy.Api.Models;

namespace Netmefy.Api.Controllers.api
{
    public class routersController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        

        private Models.routerInfoModel getRouterInfo(int router_sk)
        {
            router router = db.routers.Where(x => x.router_sk == router_sk).FirstOrDefault();
            List<dispositivo> devices = db.dispositivos.Where(x => x.router_sk == router.router_sk).ToList();
            List<Models.dispositivoInfoModel> devicesModel = Models.dispositivoInfoModel.ConvertTo(devices);
            List<lk_web> webs = router.lk_web.ToList();
            List<Models.webModel> websModel = new List<Models.webModel>();
            foreach (var w in webs)
            {
                websModel.Add(new Models.webModel
                {
                    ip = w.web_ip,
                    url = w.web_url,
                    id = w.web_sk,
                    nombre = w.web_nombre
                });
            }

            Models.routerInfoModel routerFinal = new Models.routerInfoModel();
            routerFinal.router_sk = router.router_sk;
            routerFinal.modelo = router.router_modelo;
            routerFinal.ssid = router.router_ssid;
            routerFinal.password = router.router_psw;
            routerFinal.devices = devicesModel;
            routerFinal.webs_bloqueadas = websModel;

            return routerFinal;

        }

        // GET: api/routers/5
        [ResponseType(typeof(Models.routerInfoModel))]
        [HttpGet]
        
        //public IHttpActionResult Getrouter(Models.routerInfoModel r)
        public IHttpActionResult Getrouter(int router_sk)
        {
            Models.routerInfoModel router = getRouterInfo(router_sk);

            if (router == null)
            {
                return NotFound();
            }
            return Ok(router);

        }


        // PUT: api/routers/5
        [ResponseType(typeof(Models.router2Model))]
        [HttpPut]
        public IHttpActionResult updatearRouter(Models.router2Model router)
        {

            try
            {
                router r = db.routers.Where(x => x.router_sk == router.router_sk).FirstOrDefault();

                r.router_modelo = router.router_modelo;
                r.router_psw = router.router_psw;
                r.router_ssid = router.router_ssid;

                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { status = "ok" }, new { ot = router });
            }
            catch (Exception ex)
            {
                return CreatedAtRoute("DefaultApi", new { status = "error" }, new { mensaje = ex.ToString() });
            }
        }


        // POST: api/routers
        [ResponseType(typeof(Models.webModel))]
        public IHttpActionResult Postrouter(Models.webBloqModel webBloq)
        {
            try
            {
                router routerModel = db.routers.Where(z => z.router_sk == webBloq.router_sk).FirstOrDefault();
                int cant_bloq = 0;
                int cant_no_bloq = 0;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<webABloquearModel> webABloquearModelString = serializer.Deserialize<List<webABloquearModel>>(webBloq.webs);

                foreach (Models.webABloquearModel webok in webABloquearModelString)
                {
                    Data.lk_web web;
                    web = db.lk_web.Where(x => x.web_sk == webok.web_sk).FirstOrDefault();
                    if (webok.web_bloqueado)
                    {
                        routerModel.lk_web.Add(web);
                        cant_bloq++;
                    }
                    else
                    {
                        routerModel.lk_web.Remove(web);
                        cant_no_bloq++;
                    }

                }

                db.SaveChanges();
                Models.routerInfoModel routerModelfinal = getRouterInfo(webBloq.router_sk);

                return CreatedAtRoute("DefaultApi", new { cant = cant_no_bloq + cant_bloq }, new {status="ok", cant_bloq = cant_bloq, cant_no_bloq = cant_no_bloq, router= routerModelfinal });

            }
            catch (Exception ex)
            {
                return CreatedAtRoute("DefaultApi", new { id = 123 }, new { status="error:"+ ex.ToString(), data= webBloq});
            }


        }
    }
}