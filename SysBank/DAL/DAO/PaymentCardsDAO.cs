using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysBank.BLL.Models;
using System.Threading;

namespace SysBank.DAL.DAO
{
    public class PaymentCardsDAO
    {
        SysBankEntities context = new SysBankEntities();

        public List<PaymentCards> GetAllPaymentCardsForUser(string id)
        {
            return context.PaymentCards.Where(x => x.UserId == id).ToList();
        }

        public DebitCard GetDebitCardForAccount(int id)
        {
            return context.DebitCard.Where(x => x.AccountId == id).SingleOrDefault();
        }

        public PaymentCards GetPaymentCardById(int id)
        {
            return context.PaymentCards.Where(x => x.Id == id).Single();
        }

        public ATMCard GetATMCardById(int id)
        {
            return context.ATMCard.Where(x => x.BaseCardId == id).Single();
        }

        public CreditCard GetCreditCardById(int id)
        {
            return context.CreditCard.Where(x => x.BaseCardId == id).Single();
        }

        public DebitCard GetDebitCardById(int id)
        {
            return context.DebitCard.Where(x => x.BaseCardId == id).Single();
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

        public int CreateBaseCard(PaymentCardApplication model)
        {
            try
            {
                Random rand = new Random();
                PaymentCards card = new PaymentCards()
                {
                    UserId = model.UserId,
                    TypeId = model.TypeId,
                    CreationDate = DateTime.Now,
                    CardSecurityCode = rand.Next(100, 999).ToString(),
                    ExpirationDate = DateTime.Now.AddYears(3),
                    IsBlocked = false,
                    PIN = rand.Next(1000, 9999),
                    CardNumber = CreateFakeCreditCardNumber("22", 13)
                };
                context.Entry(card).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();                
                return card.Id;
            }
            catch (Exception ex) { return 0; }

        }

        public int CreateCreditCard(CreditCardApplication model, int baseId)
        {
            try
            {
                CreditCard card = new CreditCard()
                {
                    BaseCardId = baseId,
                    Debit = 0,
                    GracePeriod = 30,
                    GracePeriodCount = 0,
                    InterestRate = (decimal)1.5,
                    IsContactless = model.IsContactless,
                    Limit = model.Limit,
                    MinimalRepayment = 150,
                    Provision = 0

                };
                
                context.Entry(card).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return card.Id;
            }
            catch (Exception ex) { return 0; }
        }

        public int CreateDebitCard(DebitCardApplication model, int baseId)
        {
            DebitCard card = new DebitCard()
            {
                BaseCardId = baseId,
                MonthlyLimit = 300,
                AccountId = model.AccountId,
                IsContactless = model.IsContactless
            };

            context.Entry(card).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return card.Id;
        }

        public int CreateATMCard(ATMCardApplication model, int baseId)
        {
            ATMCard card = new ATMCard()
            {
                BaseCardId = baseId,
                AccountId = model.AccountId,
                DailyLimit = model.DailyLimit
            };

            context.Entry(card).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
            return card.Id;
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

        public List<PaymentCardApplication> GetAllApplications()
        {
            return context.PaymentCardApplication.Where(x => x.DecisionId == null).ToList();
        }

        public PaymentCardApplication GetBaseApplication(int id)
        {
            return context.PaymentCardApplication.Where(x => x.Id == id).Single();
        }

        public ATMCardApplication GetATMApplication(int id)
        {
            return context.ATMCardApplication.Where(x => x.BaseApplicationId == id).Single();
        }

        public DebitCardApplication GetDebitApplication(int id)
        {
            return context.DebitCardApplication.Where(x => x.BaseApplicationId == id).Single();
        }

        public CreditCardApplication GetCreditApplication(int id)
        {
            return context.CreditCardApplication.Where(x => x.BaseApplicationId == id).Single();
        }

        public void ApplicationAccept(int id)
        {
            var item = context.PaymentCardApplication.Where(x => x.Id == id).Single();
            item.DecisionId = 2001;
            item.DecisionDate = DateTime.Now;
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void ApplicationReject(int id)
        {
            var item = context.PaymentCardApplication.Where(x => x.Id == id).Single();
            item.DecisionId = 2002;
            item.DecisionDate = DateTime.Now;
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public List<PaymentCardsOperationHistory> GetOperationsHistory()
        {
            return context.PaymentCardsOperationHistory.ToList();
        }
        public void CreateOperationHistory(PaymentCardsOperationHistory model)
        {
            context.Entry(model).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
        }

        public void WithdrawMoneyFromAccount(int id, decimal cashAmount)
        {
            var item = context.Accounts.Where(x => x.Id == id).Single();
            item.CurrentBalance -= cashAmount;
            item.AvailableBalance -= cashAmount;
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void DepositMoneyToAccount(int id, decimal cashAmount)
        {
            var item = context.Accounts.Where(x => x.Id == id).Single();
            item.CurrentBalance += cashAmount;
            item.AvailableBalance += cashAmount;
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void UpdateCreditCard(CreditCardModel model)
        {
            var card = GetCreditCardById(model.BaseCardId);
            card.Debit = model.Debit;
            card.Provision = model.Provision;
            context.Entry(card).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        #region helpers
        private static string CreateFakeCreditCardNumber(string prefix, int length)
        {
            string ccnumber = prefix;

            while (ccnumber.Length < (length - 1))
            {
                double rnd = (new Random().NextDouble() * 1.0f - 0f);

                ccnumber += Math.Floor(rnd * 10);

                //sleep so we get a different seed

                Thread.Sleep(20);
            }


            // reverse number and convert to int
            var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();

            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

            // calculate sum

            int sum = 0;
            int pos = 0;
            int[] reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < length - 1)
            {
                int odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != (length - 2))
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }

            // calculate check digit
            int checkdigit =
                Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;

            ccnumber += checkdigit;

            return ccnumber;
        }
        #endregion


    }
}
