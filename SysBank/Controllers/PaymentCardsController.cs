using SysBank.BLL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SysBank.BLL.Models;
namespace SysBank.Controllers
{
    public class PaymentCardsController : Controller
    {
        PaymentCardsFcd cardsFcd = new PaymentCardsFcd();
        UsersFcd usersFcd = new UsersFcd();
        // GET: PaymentCards
        public ActionResult Index()
        {
            return View(cardsFcd.GetAllPaymentCardsForUser(User.Identity.GetUserId()));
        }

        public ActionResult CardDetails(int id)
        {
            return View(cardsFcd.GetPaymentCardById(id));
        }
        [HttpGet]
        public ActionResult BaseCardApplication()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "-- Wybierz typ karty kredytowej --", Value = "0" });
            items.Add(new SelectListItem() { Text = "Karta kredytowa", Value = "1001" });
            items.Add(new SelectListItem() { Text = "Karta debetowa", Value = "1002" });
            items.Add(new SelectListItem() { Text = "Karta bankomatowa", Value = "1003" });

            List<SelectListItem> accounts = new List<SelectListItem>();
            accounts = usersFcd.GetUserAccountsByUserId(User.Identity.GetUserId()).Select(
                x => new SelectListItem()
                {
                    Text = x.AccountNumber,
                    Value = x.Id.ToString()
                }).ToList();

            BaseCardApplicationModel baseCardApplication = new BaseCardApplicationModel()
            {
                CardTypes = items,
                CreditCardApplication = new CreditCardApplication(),
                DebitCardApplication = new DebitCardApplication() {Accounts = accounts },
                ATMCardApplication = new ATMCardApplication() { Accounts = accounts },
                HasAnyAccount = accounts.Count > 0 ? true : false
            };
            return View(baseCardApplication);
        }

        [HttpPost]
        public ActionResult BaseCardApplication(BaseCardApplicationModel model)
        {
            model.CreationDate = DateTime.Now;
            model.UserId = User.Identity.GetUserId();
            cardsFcd.CreateApplication(model);

            return Redirect("~/PaymentCards/BaseCardApplication");
        }

        public ActionResult ApplicationList()
        {
           return View(cardsFcd.GetApplicationsByUserId(User.Identity.GetUserId()));
        }
    }
}