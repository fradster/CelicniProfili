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
    
    public partial class Profil_I_karakteristike
    {
				public int ID { get; set; }
        public double A { get; set; }
        public double G { get; set; }
        public double Ix { get; set; }
        public double Wx { get; set; }
        public double ix_jez { get; set; }
        public double Iy { get; set; }
        public double Wy { get; set; }
        public double iy_jez { get; set; }
        public double Sx { get; set; }
        public double s_x { get; set; }
        public double I_tor { get; set; }
    
        public virtual profil profil { get; set; }
    }
}
