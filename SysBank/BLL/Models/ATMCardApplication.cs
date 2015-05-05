using System;
using System.Collections.Generic;
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
        public decimal DailyLimit { get; set; }
        public List<SelectListItem> Accounts { get; set; }

        public int AccountId { get; set; }
    }
}
