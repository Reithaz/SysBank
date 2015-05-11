using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SysBank.BLL.Models
{
    public class PaymentCardsModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
        [Display(Name="Blokada karty")]
        public string IsBlockedString { get { if (IsBlocked) return "Tak"; else return "Nie"; } }
        public DateTime CreationDate { get; set; }
        [Display(Name="Data utworzenia")]
        public string CreationDateShort { get { return CreationDate.ToShortDateString(); } }
        
        public int TypeId { get; set; }
        [Display(Name = "Typ karty")]
        public string Type { get; set; }
    }
}
