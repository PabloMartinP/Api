﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class tecnicoModel
    {
        public int sk { get; set; }
        public string id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public decimal?  calificacion { get; set; }
        public List<tecnicoOtModel> ots { get; set; }
    }

    public class tecnicoOtModel
    {
        public int ot_id { get; set; }
        public string tipo_ot { get; set; }
        public int cliente_sk { get; set; }
        public string cliente_desc { get; set; }
        public string cliente_direccion { get; set; }
        public string cliente_tipo_casa { get; set; }
        public int estado { get; set; }
        public string estado_desc { get; set; }
        public string fecha { get; set; }
    }
}