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
    
    public partial class DebitCard
    {
        public int Id { get; set; }
        public int BaseCardId { get; set; }
        public bool IsContactless { get; set; }
        public decimal MonthlyLimit { get; set; }
        public int AccountId { get; set; }
        public Nullable<decimal> MonthlySpendings { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual PaymentCards PaymentCards { get; set; }
    }
}
