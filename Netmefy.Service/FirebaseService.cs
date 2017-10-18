using Netmefy.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Netmefy.Service
{
    public class FirebaseService
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        private string ServerKey = "AAAAgDTI05M:APA91bEoOR5n7NvFAESzgS53M-Zu9WEq0-nWwPhIjHZ4TWw2RaNl9JfnYlEAXMcwP6pblodyS6n8ZgeGTz1Wbq8rh4wkqPouc59OfzjUt8KkCsnKjs0SwjuUySmESeHtqTrdABf7PJTP";
        private string SenderId = "550641390483";
        private string UrlPushNotification = "https://fcm.googleapis.com/fcm/send";

        public bool registerTokenIfNotExists(token token)
        {
            Data.token tokenFound = db.tokens.Where(x => x.sk_entidad == token.sk_entidad && x.es_cliente && x.tokenid.Equals(token.tokenid)).FirstOrDefault();

            if(tokenFound == null)
            {
                db.tokens.Add(token);
                db.SaveChanges();
                return true;
            }
            return false;
            
        }

        public class notificacion_mensaje
        {
            public string titulo { get; set; }
            public string descripcion { get; set; }
            public int cliente_sk { get; set; }
            public int usuario_sk { get; set; }
        }
        public class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<FCMResult> results { get; set; }
        }
        public class FCMResult
        {
            public string message_id { get; set; }
        }
        public WebRequest createWebRequestPush()
        {
            WebRequest tRequest = WebRequest.Create(UrlPushNotification);
            tRequest.Method = "post";

            tRequest.Headers.Add(string.Format("Authorization: key={0}", ServerKey));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SenderId));
            tRequest.ContentType = "application/json";

            return tRequest;
        }

        private string crearParamsNotificaciones(notificacion_mensaje mensaje)
        {
            bool enviar_sin_enviar = false;//true para desarrollar. simula el envio
            //string priority = "normal";
            string priority = "high";
            //si es mensaje para un topic            
                string[] registration_ids;

                registration_ids =  getRegistrationIds(mensaje);
                /////////////////////////////////////////////////
                var objNotification = new
                {
                    registration_ids = registration_ids,
                    priority = priority,
                    content_available = true,
                    dry_run = enviar_sin_enviar,
                    notification = new
                    {
                        body = mensaje.descripcion,
                        title = mensaje.titulo,
                        icon = "myicon",
                        sound = "default"
                    }
    };

            return Newtonsoft.Json.JsonConvert.SerializeObject(objNotification);

        }

        private List<token> ObtenerTokenPorUsernameCliente(int cliente_id )
        {
            List<Data.token> result;
            result = db.tokens.Where(d => d.sk_entidad == cliente_id).ToList();

            return result;
        }

        private string[] getRegistrationIds(notificacion_mensaje notificacion_mensaje)
        {
            List<token> nts;
            nts = this.ObtenerTokenPorUsernameCliente(notificacion_mensaje.cliente_sk);
            

            List<string> registration_ids = new List<string>();
            foreach (token nt in nts)
            {
                registration_ids.Add(nt.tokenid);
            }
            return registration_ids.ToArray();
        }

        
        public bool EnviarAFCM (notificacion_mensaje mensaje)
        {
            if (mensaje.cliente_sk != 0) { 

                WebRequest tRequest = createWebRequestPush();
                string jsonNotificationFormat = crearParamsNotificaciones(mensaje);

                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();
                                //LogRepository.LOG_INFO((int)mensaje.doc_tipo, mensaje.doc_numero, responseFromFirebaseServer);                                


                                FCMResponse fcmResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                                //como manda a muchos es dificil saber si ejecuto todos bien
                                return true;                            
                                //return fcmResponse.success !=0;
                          


                            }
                        }
                    }
                }
            } else
            {
                // CODIGO DE LA NOTI X USUARIO
                return true;
            }

        }

    }
}