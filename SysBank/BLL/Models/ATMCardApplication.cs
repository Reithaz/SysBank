using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SysBank.BLL.Models
{
    public class ATMCardApplication
    {
        public int Id { get; set; }
        public int BaseApplicationId { get; set; }
        [Display(Name="Dzienny limit")]
        public decimal DailyLimit { get; set; }
        
        public List<SelectListItem> Accounts { get; set; }
        [Display(Name = "Powiązany numer rachunku")]
        public int AccountId { get; set; }
        [Display(Name = "Powiązany numer rachunku")]
        public string AccountNumber { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
    }
}
