//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Netmefy.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_ot_abiertas
    {
        public int ot_id { get; set; }
        public string tipo_ot { get; set; }
        public Nullable<int> tecnico_sk { get; set; }
        public int cliente_sk { get; set; }
        public string cliente_desc { get; set; }
        public string cliente_direccion { get; set; }
        public string cliente_tipo_casa { get; set; }
        public int estado { get; set; }
        public string estado_desc { get; set; }
        public string fecha { get; set; }
    }
}
