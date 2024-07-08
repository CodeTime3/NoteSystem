namespace NoteSystemDao.DbItems
{
    [NameTable("tokens")]
    public class TokenDbItem
    {
        [IgnoreColumn]
        public int TokenId { get; set; }
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