using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSystemDao.DbItems
{
    public class TokenDbItem
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string TokenValue { get; set; } = string.Empty;
        public DateTime TokenDate { get; set; }
    }
}
