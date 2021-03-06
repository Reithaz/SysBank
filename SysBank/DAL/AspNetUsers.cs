//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaims>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogins>();
            this.AspNetRoles = new HashSet<AspNetRoles>();
            this.PaymentCardApplication = new HashSet<PaymentCardApplication>();
            this.PaymentCards = new HashSet<PaymentCards>();
            this.Accounts = new HashSet<Accounts>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string Apartament { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public System.DateTime PlaceOfBirth { get; set; }
        public string Pesel { get; set; }
        public string Nip { get; set; }
        public string IdentificationNumber { get; set; }
        public string Regon { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
        public virtual ICollection<PaymentCardApplication> PaymentCardApplication { get; set; }
        public virtual ICollection<PaymentCards> PaymentCards { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
