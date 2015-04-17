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
    }
}