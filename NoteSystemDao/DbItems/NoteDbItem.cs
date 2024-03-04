using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSystemDao.DbItems
{
    public class NoteDbItem
    {
        public int NoteId { get; set; }
        public int UserId { get; set; } //decidere se usare la classe o la stringa
        public string NoteTitle { get; set; } = string.Empty;
        public string NoteText { get; set; } = string.Empty;
    }
}
