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
            return new PaymentCardsModel() { UserId = card.UserId, CreationDate = card.CreationDate, Id = card.Id, IsBlocked = card.IsBlocked, TypeId = card.TypeId, CardNumber = card.CardNumber, ExpirationDate = card.ExpirationDate };
        }

        public ATMCardModel GetATMCardById(int id)
        {           
            var atmCard = cardsDAO.GetATMCardById(id);            
            var baseCard = GetPaymentCardById(id);
            var account = usersFcd.GetUserAccountById(atmCard.AccountId);

            ATMCardModel atmCardModel = new ATMCardModel()
            {
                AccountId = atmCard.AccountId,
                BaseCardId = atmCard.BaseCardId,
                DailyLimit = atmCard.DailyLimit,
                Id = atmCard.Id,
                BaseCard = baseCard,
                UsedLimit = CalculateDailyUsedLimit(id),
                AvailableBalance = account.AvailableBalance,
                UserId = baseCard.UserId,
                BoundAccountNumber = account.AccountNumber
            };

            return atmCardModel;
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

        public decimal CalculateDailyUsedLimit(int cardId)
        {
            string today = DateTime.Now.ToShortDateString();
            decimal limit = 0;
            var operations = cardsDAO.GetOperationsHistory().Where(x => x.OperationTypeId == 3002 && x.CreditCardId == cardId && x.DateIssued.ToShortDateString().Equals(today)).ToList();
            foreach (var item in operations)
            {
                limit += item.BalanceChange*(-1);
            }
            return limit;
        }

        public void ATMWithdrawMoney(ATMCardModel atmcard)
        {
            using (var ts = new TransactionScope())
            {
                cardsDAO.WithdrawMoneyFromAccount(atmcard.AccountId, atmcard.CashAmount);
                cardsDAO.DepositMoneyToAccount((int)Consts.Consts.AccountNumbers.SystemAccountId, atmcard.CashAmount);
                cardsDAO.CreateOperationHistory(new DAL.PaymentCardsOperationHistory()
                {
                    BalanceChange = atmcard.CashAmount * (-1),
                    CreditCardId = atmcard.Id,
                    CreditCardTypeId = 1003,
                    DateIssued = DateTime.Now,
                    OperationTypeId = 3002,
                    UserId = atmcard.UserId
                });
                ts.Complete();
            }
        }

        public void ATMDepositMoney(ATMCardModel atmcard)
        {
            using (var ts = new TransactionScope())
            {
                cardsDAO.DepositMoneyToAccount(atmcard.AccountId, atmcard.CashAmount);
                cardsDAO.WithdrawMoneyFromAccount((int)Consts.Consts.AccountNumbers.SystemAccountId, atmcard.CashAmount);
                cardsDAO.CreateOperationHistory(new DAL.PaymentCardsOperationHistory()
                {
                    BalanceChange = atmcard.CashAmount,
                    CreditCardId = atmcard.Id,
                    CreditCardTypeId = 1003,
                    DateIssued = DateTime.Now,
                    OperationTypeId = 3001,
                    UserId = atmcard.UserId
                });
                ts.Complete();
            }
        }

        public List<PaymentCardOperationHistoryModel> GetPaymentCardsOperationHistory(string userId)
        {
            var list = cardsDAO.GetOperationsHistory().Where(x => x.UserId == userId).Select(x => new PaymentCardOperationHistoryModel()
            {
               AccountNumber = "",
               BalanceChange = x.BalanceChange,
               CreditCardId = x.CreditCardId,
               CreditCardType = dictFcd.GetDictionaryItem(x.CreditCardTypeId).Value,
               CreditCardTypeId = x.CreditCardTypeId,
               DateIssued = x.DateIssued,
               Id = x.Id,
               OperationType = dictFcd.GetDictionaryItem(x.OperationTypeId).Value,
               OperationTypeId = x.OperationTypeId
            }).ToList();


            return list;
        }
    }
}
