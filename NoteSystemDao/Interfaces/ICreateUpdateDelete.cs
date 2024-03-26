using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSystemDao.Interfaces
{
    public interface ICreateUpdateDelete<T>
    {
        int AddNewItem(T item);

        int UpdateItem(T item);

        int DeleteItem(int id);
    }
}
