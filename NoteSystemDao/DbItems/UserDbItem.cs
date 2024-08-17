namespace NoteSystemDao.DbItems
{
    [NameTable("users")]
    public class UserDbItem
    {
        public int UserId { get; set; }
        [Search]
        public string UserName { get; set; }
        public string UserHash { get; set; }

        public UserDbItem() { }

        public UserDbItem (string userName, string userHash)
        {
            UserName = userName;
            UserHash = userHash;
        }
    }
}