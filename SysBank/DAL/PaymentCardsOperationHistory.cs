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
    
    public partial class PaymentCardsOperationHistory
    {
        public int Id { get; set; }
        public int OperationTypeId { get; set; }
        public int CreditCardId { get; set; }
        public int CreditCardTypeId { get; set; }
        public System.DateTime DateIssued { get; set; }
        public decimal BalanceChange { get; set; }
        public string UserId { get; set; }
    
        public virtual DictionaryItems DictionaryItems { get; set; }
        public virtual DictionaryItems DictionaryItems1 { get; set; }
        public virtual PaymentCards PaymentCards { get; set; }
    }
}
