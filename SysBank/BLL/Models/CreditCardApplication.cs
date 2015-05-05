using System;
using System.Collections.Generic;
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
        public bool IsContactless { get; set; }
    }
}
