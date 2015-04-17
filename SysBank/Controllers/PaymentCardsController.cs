using SysBank.BLL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace SysBank.Controllers
{
    public class PaymentCardsController : Controller
    {
        PaymentCardsFcd cardsFcd = new PaymentCardsFcd();
        // GET: PaymentCards
        public ActionResult Index()
        {
            return View(cardsFcd.GetAllPaymentCardsForUser(User.Identity.GetUserId()));
        }

        public ActionResult CardDetails(int id)
        {
            return View(cardsFcd.GetPaymentCardById(id));
        }
    }
}