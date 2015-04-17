using SysBank.BLL.Facades;
using SysBank.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysBank.Controllers
{
    public class UsersController : Controller
    {
        private UsersFcd _usersFcd = new UsersFcd();
        // GET: Users
        public ActionResult Index()
        {
            List<UsersModel> usersList = _usersFcd.GetAllUsers();
            return View(usersList);
        }
    }
}