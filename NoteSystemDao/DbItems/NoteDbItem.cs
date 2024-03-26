namespace NoteSystemDao.DbItems
{
    public class NoteDbItem
    {
        public int NoteId { get; set; }
        public int UserId { get; set; } 
        public string NoteTitle { get; set; } = string.Empty;
        public string NoteText { get; set; } = string.Empty;

        public NoteDbItem() { }

        public NoteDbItem(int userId, string noteTitle, string noteText)
        {   
            UserId = userId;
            NoteTitle = noteTitle;
            NoteText = noteText;    
        }
    }
}
