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
    
    public partial class CreditCardApplication
    {
        public int Id { get; set; }
        public decimal Limit { get; set; }
        public bool IsContactless { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MinimalRepayment { get; set; }
        public int BaseApplicationId { get; set; }
    
        public virtual PaymentCardApplication PaymentCardApplication { get; set; }
    }
}
