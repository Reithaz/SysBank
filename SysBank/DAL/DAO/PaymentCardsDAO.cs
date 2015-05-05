using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysBank.BLL.Models;

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

        public int CreateBaseApplication(BaseCardApplicationModel model)
        {
            PaymentCardApplication application = new PaymentCardApplication()
            {
                UserId = model.UserId,
                TypeId = model.CardTypeId,
                CreationDate = model.CreationDate
            };
            context.Entry(application).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return application.Id;
        }

        public int CreateCreditCardApplication(BLL.Models.CreditCardApplication model)
        {
            CreditCardApplication application = new CreditCardApplication()
            {
                BaseApplicationId = model.BaseApplicationId,
                InterestRate = 0,
                IsContactless = model.IsContactless,
                Limit = model.Limit,
                MinimalRepayment = 100
            };
            context.Entry(application).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return application.Id;
        }

        public int CreateDebitCardApplication(BLL.Models.DebitCardApplication model)
        {
            DebitCardApplication application = new DebitCardApplication()
            {
                BaseApplicationId = model.BaseApplicationId,
                IsContactless = model.IsContactless,
                AccountId = model.AccountId
            };
            context.Entry(application).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return application.Id;
        }

        public int CreateATMCardApplication(BLL.Models.ATMCardApplication model)
        {
            ATMCardApplication application = new ATMCardApplication()
            {
                BaseApplicationId = model.BaseApplicationId,
                DailyLimit = model.DailyLimit,
                AccountId = model.AccountId
            };
            context.Entry(application).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return application.Id;
        }

        public List<PaymentCardApplication> GetApplicationsByUserId(string userId)
        {
            return context.PaymentCardApplication.Where(x => x.UserId == userId).ToList();
        }
    }
}
