using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSystemDao.DbItems
{
    public class UserDbItem
    { 
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserHash { get; set; }
    }
}
