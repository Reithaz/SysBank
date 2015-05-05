using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SysBank.BLL.Models
{
    public class DebitCardApplication
    {
        public int Id { get; set; }
        public int BaseApplicationId { get; set; }
        public bool IsContactless { get; set; }
        public List<SelectListItem> Accounts { get; set; }
        public int AccountId { get; set; }
    }
}
