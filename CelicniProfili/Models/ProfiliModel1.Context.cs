﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ČeličniProfiliEntities : DbContext
    {
        public ČeličniProfiliEntities()
            : base("name=ČeličniProfiliEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<I_geometrija> I_geometrija { get; set; }
        public virtual DbSet<I_karakteristike> I_karakteristike { get; set; }
        public virtual DbSet<Monoblok> Monoblok { get; set; }
        public virtual DbSet<ojačanje_opis> ojačanje_opis { get; set; }
        public virtual DbSet<profil> profil { get; set; }
        public virtual DbSet<Profil_I_karakteristike> Profil_I_karakteristike { get; set; }
        public virtual DbSet<standard> standard { get; set; }
        public virtual DbSet<UserActivation> UserActivation { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Admin_SMTP_parameteres> Admin_SMTP_parameteres { get; set; }
        public virtual DbSet<tehnologija_monoblok> tehnologija_monoblok { get; set; }
        public virtual DbSet<tip_monoblok> tip_monoblok { get; set; }
    }
}
