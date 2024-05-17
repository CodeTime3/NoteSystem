namespace NoteSystemDao.DbItems
{
    public class NoteDbItem
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }

        public NoteDbItem() { }

        public NoteDbItem (int userId, string noteTitle, string noteText)
        {
            UserId = userId;
            NoteTitle = noteTitle;
            NoteText = noteText;
        }
    }
}