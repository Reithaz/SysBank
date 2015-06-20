using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysBank.DAL.DAO;
using SysBank.BLL.Models;

namespace SysBank.BLL.Facades
{
    public class UsersFcd
    {
        private UsersDAO _usersDAO = new UsersDAO();

        public List<UsersModel> GetAllUsers()
        {
            var users = _usersDAO.GetAllUsers();

            return users.Select(x => new UsersModel() { Id = x.Id, UserName = x.UserName }).ToList();
        }

        public List<AccountsModel> GetAllAccounts()
        {
            var accounts = _usersDAO.GetAllAccounts();

            return accounts.Select(x => new AccountsModel()
            {
                AccountNumber = x.AccountNumber,
                AvailableBalance = x.AvailableBalance,
                BlockedBalance = (decimal)x.BlockedBalance,
                CurrentBalance = x.CurrentBalance,
                Id = x.Id,
                Interest = x.Interest,
                Provision = x.Provision,
                UserId = x.UserId
            }
            ).ToList();
        }

        public UsersModel GetUserById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var user = _usersDAO.GetUserById(id);
                return new UsersModel() { Id = user.Id, UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName };
            }
            else
                return new UsersModel();
        }

        public List<AccountsModel> GetUserAccountsByUserId(string id)
        {
            return _usersDAO.GetUserAccountsByUserId(id).Select(x => new AccountsModel()
            {
                Id = x.Id,
                Interest = x.Interest,
                AccountNumber = x.AccountNumber,
                AvailableBalance = x.AvailableBalance,
                CurrentBalance = x.CurrentBalance,
                Provision = x.Provision,
                UserId = x.UserId
            }).ToList();
        }

        public AccountsModel GetUserAccountById(int id)
        {
            var account = _usersDAO.GetUserAccountById(id);

            return new AccountsModel()
            {
                AccountNumber = account.AccountNumber,
                AvailableBalance = account.AvailableBalance,
                CurrentBalance = account.CurrentBalance,
                Interest = account.Interest,
                Id = account.Id,
                Provision = account.Provision,
                UserId = account.UserId,
                BlockedBalance = (decimal)account.BlockedBalance
            };
        }


    }
}