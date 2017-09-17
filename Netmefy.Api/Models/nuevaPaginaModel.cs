using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class nuevaPaginaModel
    {
        public int cliente_sk { get; set; }
        public int usuario_sk { get; set; }
        public string pagina { get; set; }
        public int id { get; set; }
    }
}