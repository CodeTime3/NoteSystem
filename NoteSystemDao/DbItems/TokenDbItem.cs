namespace NoteSystemDao.DbItems
{
    public class TokenDbItem
    {
        public int TokeId { get; set; }
        public int UserId { get; set; }
        public string TokenValue { get; set; }
        public DateTime TokenDate { get; set; }

        public TokenDbItem() { }

        public TokenDbItem (int userId, string tokenValue, DateTime tokenDate)
        {
            UserId = userId;
            TokenValue = tokenValue;
            TokenDate = tokenDate;
        }
    }
}