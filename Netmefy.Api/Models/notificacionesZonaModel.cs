using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class notificacionesZonaModel
    {
        public string notificacion_desc { get; set; }
        public string notificacion_texto { get; set; }
        public int localidad_sk { get; set; }
        public string notificacion_tipo { get; set; }
                
    }
}