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
    
    public partial class Deposites
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> GatheredInterests { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual DepositType DepositType { get; set; }
    }
}