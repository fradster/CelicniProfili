//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CelicniProfili.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Monoblok
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Monoblok()
        {
            this.profil = new HashSet<profil>();
        }
    
        public int ID { get; set; }
        public int ID_standard { get; set; }
        public string Naziv { get; set; }
        public int ID_tip { get; set; }
    
        public virtual I_geometrija I_geometrija { get; set; }
        public virtual I_karakteristike I_karakteristike { get; set; }
        public virtual standard standard { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<profil> profil { get; set; }
    }
}
