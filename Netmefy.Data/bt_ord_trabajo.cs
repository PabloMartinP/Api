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
    
    public partial class bt_ord_trabajo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bt_ord_trabajo()
        {
            this.bt_ot_status = new HashSet<bt_ot_status>();
            this.bt_tests = new HashSet<bt_tests>();
        }
    
        public int ot_id { get; set; }
        public Nullable<int> tecnico_sk { get; set; }
        public System.DateTime fh_creacion { get; set; }
        public Nullable<System.DateTime> fh_cierre { get; set; }
        public Nullable<int> calificacion { get; set; }
        public int cliente_sk { get; set; }
    
        public virtual tecnico lk_tecnico { get; set; }
        public virtual lk_tiempo lk_tiempo { get; set; }
        public virtual lk_tiempo lk_tiempo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bt_ot_status> bt_ot_status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bt_tests> bt_tests { get; set; }
    }
}
