namespace NoteSystemDao
{
    public class TokenCrud
    {
        private readonly MySqlConnection _connection;
        public TokenCrud(string connectionStr)
        {
            _connection = new MySqlConnection(connectionStr);
        }

        public int AddNewItem()
        {
            return 1;
        }
    }
}