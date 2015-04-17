using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysBank.DAL.DAO;
using SysBank.BLL.Models;

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

    }
}
