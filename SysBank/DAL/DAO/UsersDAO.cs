using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBank.DAL.DAO
{
    public class UsersDAO
    {
        SysBankEntities context = new SysBankEntities();
        public List<AspNetUsers> GetAllUsers()
        {
            return context.AspNetUsers.ToList();
        }

        public AspNetUsers GetUserById(string id)
        {
            return context.AspNetUsers.Where(x => x.Id == id).SingleOrDefault();
        }

        public List<Accounts> GetUserAccountsByUserId(string id)
        {
            return context.Accounts.Where(x => x.UserId == id).ToList();
        }

        public Accounts GetUserAccountById(int id)
        {
            return context.Accounts.Where(x => x.Id == id).Single();
        }
    }
}
