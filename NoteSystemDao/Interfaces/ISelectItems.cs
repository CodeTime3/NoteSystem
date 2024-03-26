using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSystemDao.Interfaces
{
    public interface ISelectItems<T>
    {
        T[] GetAllItems();

        T[] GetItems(string? str);
    }
}
