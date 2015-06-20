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
    
    public partial class Accounts
    {
        public Accounts()
        {
            this.ATMCard = new HashSet<ATMCard>();
            this.ATMCardApplication = new HashSet<ATMCardApplication>();
            this.DebitCard = new HashSet<DebitCard>();
            this.DebitCardApplication = new HashSet<DebitCardApplication>();
            this.Deposites = new HashSet<Deposites>();
            this.AspNetUsers = new HashSet<AspNetUsers>();
        }
    
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal Provision { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal Interest { get; set; }
        public string AccountNumber { get; set; }
        public Nullable<decimal> BlockedBalance { get; set; }
    
        public virtual ICollection<ATMCard> ATMCard { get; set; }
        public virtual ICollection<ATMCardApplication> ATMCardApplication { get; set; }
        public virtual ICollection<DebitCard> DebitCard { get; set; }
        public virtual ICollection<DebitCardApplication> DebitCardApplication { get; set; }
        public virtual ICollection<Deposites> Deposites { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
