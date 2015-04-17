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
    }
}
