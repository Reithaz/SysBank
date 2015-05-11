using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBank.BLL.Models
{
    public class CreditCardApplication
    {
        public int Id { get; set; }
        public int BaseApplicationId { get; set; }
        public decimal Limit { get; set; }
        [Display(Name="Zbliżeniowość")]
        public bool IsContactless { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
    }
}
