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
    
    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            this.bt_solicitudes = new HashSet<bt_solicitudes>();
            this.bt_tests = new HashSet<bt_tests>();
            this.lk_cliente_router = new HashSet<lk_cliente_router>();
            this.lk_usuario = new HashSet<usuario>();
        }
    
        public int cliente_sk { get; set; }
        public string cliente_id { get; set; }
        public string cliente_psw { get; set; }
        public string cliente_desc { get; set; }
        public string cliente_direccion { get; set; }
        public Nullable<int> empresa_sk { get; set; }
        public Nullable<int> localidad_sk { get; set; }
        public Nullable<int> cliente_vel_mb_contr { get; set; }
        public Nullable<int> cliente_vel_mb_umbral { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bt_solicitudes> bt_solicitudes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bt_tests> bt_tests { get; set; }
        public virtual lk_empresa lk_empresa { get; set; }
        public virtual lk_localidad lk_localidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lk_cliente_router> lk_cliente_router { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuario> lk_usuario { get; set; }
    }
}
