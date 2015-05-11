using SysBank.BLL.Models;
using SysBank.DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBank.BLL.Facades
{
    public class DictionaryFcd
    {
        DictionaryDAO dictDAO = new DictionaryDAO();
        public DictionaryItemModel GetDictionaryItem(int id)
        {
            var item = dictDAO.GetDictionaryItem(id);
            return new DictionaryItemModel()
            {
                Id = item.Id,
                Value = item.Value
            };
        }

    }
}
