using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysBank.DAL.DAO;
using SysBank.BLL.Models;
using System.Transactions;
using Microsoft.AspNet.Identity;
namespace SysBank.BLL.Facades
{

    public class PaymentCardsFcd
    {
        PaymentCardsDAO cardsDAO = new PaymentCardsDAO();
        DictionaryFcd dictFcd = new DictionaryFcd();
        UsersFcd usersFcd = new UsersFcd();
        public List<PaymentCardsModel> GetAllPaymentCardsForUser(string id)
        {
            var list = cardsDAO.GetAllPaymentCardsForUser(id);
            return list.Select(x => new PaymentCardsModel()
            {
                CreationDate = x.CreationDate,
                Id = x.Id,
                IsBlocked = x.IsBlocked,
                TypeId = x.TypeId,
                Type = dictFcd.GetDictionaryItem(x.TypeId).Value
            }).ToList();
        }

        public PaymentCardsModel GetPaymentCardById(int id)
        {
            var card = cardsDAO.GetPaymentCardById(id);
            return new PaymentCardsModel() { CreationDate = card.CreationDate, Id = card.Id, IsBlocked = card.IsBlocked, TypeId = card.TypeId };
        }

        public void CreateApplication(BaseCardApplicationModel model)
        {
            switch (model.CardTypeId)
            {
                case 1001:
                    using (var ts = new TransactionScope())
                    {
                        model.CreditCardApplication.BaseApplicationId = cardsDAO.CreateBaseApplication(model);
                        cardsDAO.CreateCreditCardApplication(model.CreditCardApplication);
                        ts.Complete();
                    }
                    break;
                case 1002:
                    using (var ts = new TransactionScope())
                    {
                        model.DebitCardApplication.BaseApplicationId = cardsDAO.CreateBaseApplication(model);
                        cardsDAO.CreateDebitCardApplication(model.DebitCardApplication);
                        ts.Complete();
                    }
                    break;
                case 1003:
                    using (var ts = new TransactionScope())
                    {
                        model.ATMCardApplication.BaseApplicationId = cardsDAO.CreateBaseApplication(model);
                        cardsDAO.CreateATMCardApplication(model.ATMCardApplication);
                        ts.Complete();
                    }
                    break;
                default:
                    throw new ApplicationException("Unexpected card type");
            }
        }

        public List<BaseCardApplicationModel> GetApplicationsByUserId(string userId)
        {
            var list = cardsDAO.GetApplicationsByUserId(userId).Select(x => new BaseCardApplicationModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                CardTypeId = x.TypeId,
                CardType = dictFcd.GetDictionaryItem(x.TypeId).Value,
                DecisionId = x.DecisionId,
                DecisionDate = x.DecisionDate,
                CreationDate = x.CreationDate,
                Decision = String.IsNullOrEmpty(x.DecisionId.ToString()) ? "Brak" : dictFcd.GetDictionaryItem((int)x.DecisionId).Value
            }).ToList();
            return list;
        }

        public BaseCardApplicationModel GetBaseApplication(int id)
        {
            var item = cardsDAO.GetBaseApplication(id);
            return new BaseCardApplicationModel()
            {
                CardTypeId = item.TypeId,
                CreationDate = item.CreationDate,
                DecisionDate = item.DecisionDate,
                DecisionId = item.DecisionId,
                Id = id,
                UserId = item.UserId
            };
        }

        public List<BaseCardApplicationModel> GetAllApplications()
        {
            var list = cardsDAO.GetAllApplications().Select(x => new BaseCardApplicationModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                CardTypeId = x.TypeId,
                CardType = dictFcd.GetDictionaryItem(x.TypeId).Value,
                DecisionId = x.DecisionId,
                DecisionDate = x.DecisionDate,
                CreationDate = x.CreationDate,
                Decision = String.IsNullOrEmpty(x.DecisionId.ToString()) ? "Brak" : dictFcd.GetDictionaryItem((int)x.DecisionId).Value
            }).ToList();
            return list;
        }

        public ATMCardApplication GetATMApplication(int id)
        {
            var item = cardsDAO.GetATMApplication(id);
            var baseItem = cardsDAO.GetBaseApplication(item.BaseApplicationId);
            var user = usersFcd.GetUserById(baseItem.UserId);

            ATMCardApplication atmApp = new ATMCardApplication()
            {
                AccountId = item.AccountId,
                AccountNumber = usersFcd.GetUserAccountById(item.AccountId).AccountNumber,
                BaseApplicationId = baseItem.Id,
                DailyLimit = item.DailyLimit,
                Id = item.Id,
                Name = user.FirstName,
                Surname = user.LastName
            };

            return atmApp;
        }

        public CreditCardApplication GetCreditApplication(int id)
        {
            var item = cardsDAO.GetCreditApplication(id);
            var baseItem = cardsDAO.GetBaseApplication(item.BaseApplicationId);
            var user = usersFcd.GetUserById(baseItem.UserId);

            CreditCardApplication creditApp = new CreditCardApplication()
            {
                IsContactless = item.IsContactless,
                Limit = item.Limit,
                BaseApplicationId = baseItem.Id,
                Id = item.Id,
                Name = user.FirstName,
                Surname = user.LastName
            };

            return creditApp;
        }

        public DebitCardApplication GetDebitApplication(int id)
        {
            var item = cardsDAO.GetDebitApplication(id);
            var baseItem = cardsDAO.GetBaseApplication(item.BaseApplicationId);
            var user = usersFcd.GetUserById(baseItem.UserId);

            DebitCardApplication debitApp = new DebitCardApplication()
            {
                IsContactless = item.IsContactless,
                AccountId = item.AccountId,
                AccountNumber = usersFcd.GetUserAccountById(item.AccountId).AccountNumber,
                BaseApplicationId = baseItem.Id,
                Id = item.Id,
                Name = user.FirstName,
                Surname = user.LastName
            };

            return debitApp;
        }

        public void ApplicationAccept(int id)
        {
            var baseApp = cardsDAO.GetBaseApplication(id);
            switch (baseApp.TypeId)
            {
                case 1001: using (var ts = new TransactionScope())
                    {
                        cardsDAO.ApplicationAccept(id);
                        int baseId = cardsDAO.CreateBaseCard(baseApp);
                        var creditCard = cardsDAO.GetCreditApplication(id);
                        cardsDAO.CreateCreditCard(creditCard, baseId);
                        ts.Complete();
                    }
                    break;
                case 1002: using (var ts = new TransactionScope())
                    {
                        cardsDAO.ApplicationAccept(id);
                        int baseId = cardsDAO.CreateBaseCard(baseApp);
                        var debitCard = cardsDAO.GetDebitApplication(id);
                        cardsDAO.CreateDebitCard(debitCard, baseId);
                        ts.Complete();
                    }
                    break;
                case 1003: using (var ts = new TransactionScope())
                    {
                        cardsDAO.ApplicationAccept(id);
                        int baseId = cardsDAO.CreateBaseCard(baseApp);
                        var ATMCard = cardsDAO.GetATMApplication(id);
                        cardsDAO.CreateATMCard(ATMCard, baseId);
                        ts.Complete();
                    }
                    break;
                default:
                    break;
            }
        }
        public void ApplicationReject(int id)
        {
            cardsDAO.ApplicationReject(id);
        }
    }
}
