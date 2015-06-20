using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBank.BLL.Models
{
    public class AccountsModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal Provision { get; set; }
        [Display(Name = "Saldo dostępne")]
        public decimal AvailableBalance { get; set; }
        public decimal Interest { get; set; }
        [Display(Name = "Numer rachunku")]
        public string AccountNumber { get; set; }
        [Display(Name = "Kwota zablokowana")]
        public decimal BlockedBalance { get; set; }
    }
}
