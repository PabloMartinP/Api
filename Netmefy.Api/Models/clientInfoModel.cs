using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class clientInfoModel
    {
        public int id;
        public string username;
        public int mb_contratado;
        public int mb_umbral;
        public string nombre;
        public routerInfoModel router { get; set; }
    }
    public class webModel
    {
        public string ip;
        public string url;
    }

    public class routerInfoModel
    {
        public int router_sk;
        public string modelo;
        public string ssid;
        public string password;
        public List<dispositivoInfoModel> devices;
        public List<webModel> webs_bloqueadas { get; set; }
    }
    public class dispositivoInfoModel
    {
        public int dispositivo_sk;
        public string mac;
        public string ip;
        public string tipo;
        public bool bloqueado;
        public string foto;
        public string apodo;

        public static List<dispositivoInfoModel> ConvertTo(List<Data.dispositivo> dispositivos)
        {
            List<dispositivoInfoModel> list = new List<dispositivoInfoModel>();

            foreach (Data.dispositivo d in dispositivos)
            {
                list.Add(new dispositivoInfoModel {
                    dispositivo_sk = d.dispositivo_sk,
                    mac = d.dispositivo_mac,
                    ip = d.dispositivo_ip, 
                    tipo = d.dispositivo_tipo, 
                    bloqueado = d.dispositivo_bloq == 0, 

                });
            }

            return list;
        }
    }
}