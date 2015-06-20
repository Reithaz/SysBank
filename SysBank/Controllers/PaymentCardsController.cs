using SysBank.BLL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SysBank.BLL.Models;
using System.Reflection;
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
            items.Add(new SelectListItem() { Text = "-- Wybierz typ karty płatniczej --", Value = "0" });
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

            return Redirect("~/PaymentCards/ApplicationList");
        }

        public ActionResult ApplicationList()
        {
            var list = cardsFcd.GetApplicationsByUserId(User.Identity.GetUserId());
            
           return View(list);
        }

        public ActionResult ApplicationAcceptanceList()
        {
            var list = cardsFcd.GetAllApplications();

            return View(list);
        }
        [HttpGet]
        public ActionResult CreditApplicationAcceptance(int id)
        {
            var app = cardsFcd.GetCreditApplication(id);

            return View(app);
        }
        [HttpGet]
        public ActionResult DebitApplicationAcceptance(int id)
        {
            var app = cardsFcd.GetDebitApplication(id);

            return View(app);
        }
        [HttpGet]
        public ActionResult ATMApplicationAcceptance(int id)
        {
            var app = cardsFcd.GetATMApplication(id);

            return View(app);
        }

        public ActionResult ApplicationAccept(int id)
        {
            cardsFcd.ApplicationAccept(id);
            return Redirect("~/PaymentCards/ApplicationAcceptanceList");

        }
        public ActionResult ApplicationReject(int id)
        {
            cardsFcd.ApplicationReject(id);
            return Redirect("~/PaymentCards/ApplicationAcceptanceList");
        }


        public ActionResult CreditCardSim(int id)
        {
            return View();
        }

        public ActionResult DebitCardSim(int id)
        {
            return View();
        }

        public ActionResult ATMCardSim(int id, string errorDetails)
        {
            var cardModel = cardsFcd.GetATMCardById(id);
            cardModel.ErrorDetails = errorDetails;
            return View(cardModel);
        }
        [HttpPost]
        [HttpParamAction]
        public ActionResult ATMWithdrawMoney(ATMCardModel atmcard){
            atmcard.ErrorDetails = "";
            if (atmcard.CashAmount <= 0) { atmcard.ErrorDetails = "Podana kwota musi być większa od zera"; }
            else
            {
                if (atmcard.CashAmount > atmcard.AvailableBalance)
                {
                    atmcard.ErrorDetails += "Brak wystarczających środków na koncie. ";
                }
                else
                {
                    if (atmcard.CashAmount > atmcard.DailyLimit - atmcard.UsedLimit)
                    {
                        atmcard.ErrorDetails += "Przekroczono dzienny limit.";
                    }
                    else
                    {
                        cardsFcd.ATMWithdrawMoney(atmcard);
                    }
                }
            }
            return RedirectToAction("ATMCardSim", new { id = atmcard.Id, errorDetails = atmcard.ErrorDetails });
        }

        [HttpPost]
        [HttpParamAction]
        public ActionResult ATMDepositMoney(ATMCardModel atmcard)
        {
            atmcard.ErrorDetails = "";
            if (atmcard.CashAmount <= 0) { atmcard.ErrorDetails = "Podana kwota musi być większa od zera"; }
            else cardsFcd.ATMDepositMoney(atmcard);
            return RedirectToAction("ATMCardSim", new { id = atmcard.Id, errorDetails = atmcard.ErrorDetails});
        }

        [HttpGet]
        public ActionResult PaymentCardsOperationHistory()
        {
            return View(cardsFcd.GetPaymentCardsOperationHistory(User.Identity.GetUserId()));
        }
    }

    #region attributes
    public class HttpParamActionAttribute : ActionNameSelectorAttribute
    {
        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                return true;

            var request = controllerContext.RequestContext.HttpContext.Request;
            return request[methodInfo.Name] != null;
        }
    }
    #endregion
}