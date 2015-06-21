using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysBank.BLL.Models
{
    public class CreditCardModel
    {
        public int Id { get; set; }
        public decimal Limit { get; set; }
        public int GracePeriod { get; set; }
        [Display(Name = "Odsetki")]
        public decimal Provision { get; set; }
        [Display(Name = "Oprocentowanie")]
        public decimal InterestRate { get; set; }
        [Display(Name = "Zadłużenie")]
        public decimal Debit { get; set; }
        public int BaseCardId { get; set; }
        [Display(Name = "Zbliżeniowość")]
        public bool IsContactless { get; set; }
        public int GracePeriodCount { get; set; }
        [Display(Name = "Minimalna kwota spłaty")]
        public decimal MinimalRepayment { get; set; }
        public PaymentCardsModel BaseCard { get; set; }

        public List<SelectListItem> Accounts { get; set; }
        [Display(Name = "Obciążany rachunek przy spłacie")]
        public int AccountId { get; set; }
        public string ErrorDetails { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Kwota")]
        public decimal CashAmount { get; set; }
    }
}