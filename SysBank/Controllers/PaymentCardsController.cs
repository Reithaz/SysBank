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

        public ActionResult DebitCardSim(int id, string errorDetails)
        {
            var cardModel = cardsFcd.GetDebitCardById(id);
            cardModel.ErrorDetails = errorDetails;
            return View(cardModel);
        }

        public ActionResult ATMCardSim(int id, string errorDetails)
        {
            var cardModel = cardsFcd.GetATMCardById(id);
            cardModel.ErrorDetails = errorDetails;
            return View(cardModel);
        }

        public ActionResult CreditCardSim(int id, string errorDetails)
        {
            var cardModel = cardsFcd.GetCreditCardById(id);
            cardModel.ErrorDetails = errorDetails;
            List<SelectListItem> accounts = new List<SelectListItem>();
            accounts = usersFcd.GetUserAccountsByUserId(User.Identity.GetUserId()).Select(
                x => new SelectListItem()
                {
                    Text = String.Format("{0} (Dostępne środki: {1} zł)", x.AccountNumber, (x.AvailableBalance - x.BlockedBalance).ToString().Remove((x.AvailableBalance - x.BlockedBalance).ToString().Length - 1)),
                    Value = x.Id.ToString()
                }).ToList();
            cardModel.Accounts = accounts;
            return View(cardModel);
        }
        [HttpPost]
        [HttpParamAction]
        public ActionResult PayWithDebit(DebitCardModel debitCard) 
        {
            debitCard.ErrorDetails = "";
            if (debitCard.CashAmount <= 0) { debitCard.ErrorDetails = "Podana kwota musi być większa od zera"; }
            else
            {
                if (debitCard.CashAmount > debitCard.AvailableBalance - debitCard.BlockedCashAmount)
                {
                    debitCard.ErrorDetails += "Podana kwota przekracza sumę salda dostępnego i kwoty zablokowanej. ";
                }
                else
                {
                    if (debitCard.CashAmount > debitCard.MonthlyLimit - debitCard.UsedMonthlyLimit)
                    {
                        debitCard.ErrorDetails += "Przekroczono miesięczny limit pieniężny.";
                    }
                    else
                    {
                        if (debitCard.UsedOperationsCount >= debitCard.OperationsCount)
                        {
                            debitCard.ErrorDetails += "Przekroczono dzienny limit ilości operacji";
                        }
                        else cardsFcd.PayWithDebit(debitCard);
                    }
                }
            }

            return RedirectToAction("DebitCardSim", new { id = debitCard.Id, errorDetails = debitCard.ErrorDetails });      
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
                        atmcard.ErrorDetails += "Przekroczono dzienny limit pieniężny.";
                    }
                    else
                    {
                        if (atmcard.UsedOperationsCount >= atmcard.OperationsCount)
                        {
                            atmcard.ErrorDetails += "Przekroczono dzienny limit ilości operacji";
                        }else  cardsFcd.ATMWithdrawMoney(atmcard);
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

        [HttpGet]
        public ActionResult DebitTransactionAcceptance()
        {
            return View(usersFcd.GetAllAccounts().Where(x => x.BlockedBalance > 0));
        }

        public ActionResult AcceptDebitTransaction(int id)
        {
            cardsFcd.AcceptDebitTransaction(id);
            return Redirect("~/PaymentCards/DebitTransactionAcceptance");
        }

        public ActionResult RejectDebitTransaction(int id)
        {
            cardsFcd.RejectDebitTransaction(id);
            return Redirect("~/PaymentCards/DebitTransactionAcceptance");
        }
        [HttpPost]
        [HttpParamAction]
        public ActionResult PayWithCreditCard(CreditCardModel creditCard)
        {
            creditCard.ErrorDetails = "";
            if (creditCard.CashAmount <= 0) { creditCard.ErrorDetails = "Podana kwota musi być większa od zera"; }
            else
            {
                if (creditCard.CashAmount > (creditCard.Limit - creditCard.Debit)) { creditCard.ErrorDetails = "Przekroczono limit zadłużenia"; }
                else
                {
                    cardsFcd.PayWithCreditCard(creditCard);
                }
            }
            return RedirectToAction("CreditCardSim", new { id = creditCard.Id, errorDetails = creditCard.ErrorDetails });
        }

        [HttpPost]
        [HttpParamAction]
        public ActionResult PayDebtCreditCard(CreditCardModel creditCard)
        {
            creditCard.ErrorDetails = "";
            if (creditCard.AccountId == 0) { creditCard.ErrorDetails = "Nie wybrano rachunku obciążanego"; }
            else
            {
                var account = usersFcd.GetUserAccountById(creditCard.AccountId);
                if (creditCard.CashAmount <= 0) { creditCard.ErrorDetails = "Podana kwota musi być większa od zera"; }
                else
                {
                    if (creditCard.CashAmount < creditCard.MinimalRepayment) { creditCard.ErrorDetails = "Podana kwota jest mniejsza od minimalnej kwoty spłaty"; }
                    else
                    {
                        if (creditCard.CashAmount > (account.AvailableBalance - account.BlockedBalance)) { creditCard.ErrorDetails = "Brak wystarczających środków na wybranym rachunku"; }
                        else{
                            if (creditCard.CashAmount > (creditCard.Debit + creditCard.Provision)) { creditCard.ErrorDetails = "Podana kwota spłaty przekracza zadłużenie i/lub wartość odsetek"; }
                            else { cardsFcd.PayDebtCreditCard(creditCard); }
                        }
                                               
                    }
                }
            }
            return RedirectToAction("CreditCardSim", new { id = creditCard.Id, errorDetails = creditCard.ErrorDetails });
        }
        [HttpPost]
        [HttpParamAction]
        public ActionResult NextGracePeriod(CreditCardModel creditCard)
        {
            creditCard.ErrorDetails = "";
            cardsFcd.NextGracePeriod(creditCard);
            return RedirectToAction("CreditCardSim", new { id = creditCard.Id, errorDetails = creditCard.ErrorDetails });
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