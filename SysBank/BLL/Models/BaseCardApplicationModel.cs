using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SysBank.BLL.Models
{
    public class BaseCardApplicationModel
    {
        public int Id { get; set; }
        public List<SelectListItem> CardTypes { get; set; }
        public int CardTypeId { get; set; }
        [Display(Name = "Typ karty")]
        public string CardType { get; set; }
        [Display(Name="Data utworzenia")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Data decyzji")]
        public DateTime? DecisionDate { get; set; }
        public int? DecisionId { get; set; }
        [Display(Name = "Decyzja")]
        public string Decision { get; set; }
        public string UserId { get; set; }
        public bool HasAnyAccount { get; set; }

        public CreditCardApplication CreditCardApplication { get; set; }

        public DebitCardApplication DebitCardApplication { get; set; }

        public ATMCardApplication ATMCardApplication { get; set; }
    }
}
