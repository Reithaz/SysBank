﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SysBank.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SysBankEntities : DbContext
    {
        public SysBankEntities()
            : base("name=SysBankEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Application_Document> Application_Document { get; set; }
        public virtual DbSet<ATMCard> ATMCard { get; set; }
        public virtual DbSet<ATMCardApplication> ATMCardApplication { get; set; }
        public virtual DbSet<CreditCard> CreditCard { get; set; }
        public virtual DbSet<CreditCardApplication> CreditCardApplication { get; set; }
        public virtual DbSet<DebitCard> DebitCard { get; set; }
        public virtual DbSet<DebitCardApplication> DebitCardApplication { get; set; }
        public virtual DbSet<Deposites> Deposites { get; set; }
        public virtual DbSet<DepositType> DepositType { get; set; }
        public virtual DbSet<Dictionaries> Dictionaries { get; set; }
        public virtual DbSet<DictionaryItems> DictionaryItems { get; set; }
        public virtual DbSet<Insurance> Insurance { get; set; }
        public virtual DbSet<Insurance_Type> Insurance_Type { get; set; }
        public virtual DbSet<PaymentCardApplication> PaymentCardApplication { get; set; }
        public virtual DbSet<PaymentCards> PaymentCards { get; set; }
        public virtual DbSet<PaymentCardsOperationHistory> PaymentCardsOperationHistory { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
