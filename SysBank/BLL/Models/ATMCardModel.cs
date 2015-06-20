using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBank.BLL.Models
{
    public class ATMCardModel
    {
        public int Id { get; set; }
        public int BaseCardId { get; set; }
        public int AccountId { get; set; }
        [Display(Name = "Dzienny limit")]
        public decimal DailyLimit { get; set; }
        [Display(Name = "Powiązany numer rachunku")]
        public string BoundAccountNumber { get; set; }
        [Display(Name = "Wykorzystany limit")]
        public decimal UsedLimit { get; set; }
        [Display(Name = "Saldo dostępne")]
        public decimal AvailableBalance { get; set; }
        [Display(Name = "Kwota")]
        public decimal CashAmount { get; set; }

        public PaymentCardsModel BaseCard { get; set; }

        public string ErrorDetails { get; set; }
        public string UserId { get; set; }
    }
}
