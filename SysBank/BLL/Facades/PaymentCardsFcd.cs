using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysBank.DAL.DAO;
using SysBank.BLL.Models;
using System.Transactions;
namespace SysBank.BLL.Facades
{
    
    public class PaymentCardsFcd
    {
        PaymentCardsDAO cardsDAO = new PaymentCardsDAO();
        public List<PaymentCardsModel> GetAllPaymentCardsForUser(string id)
        {
            var list = cardsDAO.GetAllPaymentCardsForUser(id);
            return list.Select(x => new PaymentCardsModel()
            {
                CreationDate = x.CreationDate,
                Id = x.Id,
                IsBlocked = x.IsBlocked,
                TypeId = x.TypeId                
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
                DecisionId = x.DecisionId,
                DecisionDate = x.DecisionDate,
                CreationDate = x.CreationDate,
                Decision = String.IsNullOrEmpty(x.DecisionId.ToString()) ? "Brak" : "Jest"
            }).ToList();
            return list;
        }
    }
}
