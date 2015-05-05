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
    
    public partial class PaymentCardApplication
    {
        public PaymentCardApplication()
        {
            this.ATMCardApplication = new HashSet<ATMCardApplication>();
            this.CreditCardApplication = new HashSet<CreditCardApplication>();
            this.DebitCardApplication = new HashSet<DebitCardApplication>();
        }
    
        public int Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int TypeId { get; set; }
        public Nullable<System.DateTime> DecisionDate { get; set; }
        public Nullable<int> DecisionId { get; set; }
        public string UserId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual ICollection<ATMCardApplication> ATMCardApplication { get; set; }
        public virtual ICollection<CreditCardApplication> CreditCardApplication { get; set; }
        public virtual ICollection<DebitCardApplication> DebitCardApplication { get; set; }
        public virtual DictionaryItems DictionaryItems { get; set; }
        public virtual DictionaryItems DictionaryItems1 { get; set; }
    }
}
