﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Api.Models
{
    public class ISP_TecnicosModel
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string mail { get; set; }
        public Nullable<decimal> calificacion { get; set; }
        public int otAsignadas { get; set; }
        public int otCerradas { get; set; }
    }


}