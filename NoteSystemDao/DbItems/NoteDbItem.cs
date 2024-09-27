namespace NoteSystemDao.DbItems
{
    [NameTable("notes")]
    public class NoteDbItem
    {   
        [IgnoreColumn]
        public int NoteId { get; set; }
        [Search]
        public int UserId { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }

        public NoteDbItem() { }

        public NoteDbItem (int noteId, int userId, string noteTitle, string noteText)
        {
            NoteId = noteId;
            UserId = userId;
            NoteTitle = noteTitle;
            NoteText = noteText;
        }

        public NoteDbItem (int userId, string noteTitle, string noteText)
        {
            UserId = userId;
            NoteTitle = noteTitle;
            NoteText = noteText;
        }
    }
}