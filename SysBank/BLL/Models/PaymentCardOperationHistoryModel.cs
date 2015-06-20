using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBank.BLL.Models
{
    public class PaymentCardOperationHistoryModel
    {
        public int Id { get; set; }
        public int OperationTypeId { get; set; }
        [Display(Name="Typ operacji")]
        public string OperationType { get; set; }
        public int CreditCardId { get; set; }
        public int CreditCardTypeId { get; set; }
        [Display(Name = "Typ karty płatniczej")]
        public string CreditCardType { get; set; }
        
        public System.DateTime DateIssued { get; set; }
        [Display(Name="Data wykonania transakcji")]
        public string DateIssuedShort { get { return DateIssued.ToShortDateString(); } }
        [Display(Name="Numer powiązanego rachunku")]
        public string AccountNumber { get; set; }
        [Display(Name="Kwota")]
        public decimal BalanceChange { get; set; }
        public string UserId { get; set; }
    }
}
