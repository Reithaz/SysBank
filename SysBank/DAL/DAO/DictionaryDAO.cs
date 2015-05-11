using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysBank.BLL.Models;

namespace SysBank.DAL.DAO
{
    public class DictionaryDAO
    {
        SysBankEntities context = new SysBankEntities();

        public DictionaryItems GetDictionaryItem(int id)
        {
            return context.DictionaryItems.Where(x => x.Id == id).Single();
        }
    }
}
