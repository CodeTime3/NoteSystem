namespace NoteSystemWeb.Models
{
    public class NoteModel
    {
        public int UserId { get; set; } 
        public string NoteTitle { get; set; } = string.Empty;
        public string NoteText { get; set; } = string.Empty;
    }
}
