using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBank.DAL.DAO
{
    public class PaymentCardsDAO
    {
        SysBankEntities context = new SysBankEntities();

        public List<PaymentCards> GetAllPaymentCardsForUser(string id)
        {
            return context.PaymentCards.Where(x => x.UserId == id).ToList();
        }

        public PaymentCards GetPaymentCardById(int id)
        {
            return context.PaymentCards.Where(x => x.Id == id).Single();
        }
    }
}
