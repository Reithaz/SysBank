using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysBank.BLL.Models
{
    public class DebitCardModel
    {
        public int Id { get; set; }
        public int BaseCardId { get; set; }
        public int AccountId { get; set; }
        [Display(Name = "Limit dziennych operacji")]
        public int OperationsCount { get; set; }
        [Display(Name = "Wykorzystany dzienny limit operacji")]
        public int UsedOperationsCount { get; set; }
        [Display(Name = "Powiązany numer rachunku")]
        public string BoundAccountNumber { get; set; }
        [Display(Name = "Saldo dostępne")]
        public decimal AvailableBalance { get; set; }
        [Display(Name = "Kwota")]
        public decimal CashAmount { get; set; }
        public decimal BlockedCashAmount { get; set; }
        public PaymentCardsModel BaseCard { get; set; }
        [Display(Name="Limit miesięczny")]
        public decimal MonthlyLimit { get; set; }
        [Display(Name="Wykorzystany limit miesięczny")]
        public decimal UsedMonthlyLimit { get; set; }
        public string ErrorDetails { get; set; }
        public string UserId { get; set; }
    }
}