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
    
    public partial class I_geometrija
    {
        public int ID { get; set; }
        public double b { get; set; }
        public double h { get; set; }
        public double s { get; set; }
        public double t { get; set; }
        public Nullable<double> r1 { get; set; }
        public Nullable<double> r2 { get; set; }
    
        public virtual Monoblok Monoblok { get; set; }
    }
}
