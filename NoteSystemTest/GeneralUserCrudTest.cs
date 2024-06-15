namespace NoteSystemTest
{
    public class GeneralUserCrudTest
    {
        private static readonly string _connectionString = "server=localhost;uid=root;pwd=CT22d03p06;database=notesystem";
        private readonly MySqlConnection _connection = new MySqlConnection(_connectionString);
        private GeneralCrud<UserDbItem> _generalCrud;
        private UserDbItem _user = new UserDbItem { UserName = "pluto", UserHash = "pippo" };

        [Fact]
        public void CreateItem_should_work()
        {
            using (var connection = _connection)
            {
                connection.Open();
                _generalCrud = new GeneralCrud<UserDbItem>(connection);
                int result = _generalCrud.CreateItem(_user);

                Assert.Equal(1, result);
            }
        }

        [Fact]
        public void ReadAllItems_should_work()
        {
            using (var connection = _connection)
            {
                connection.Open();
                UserDbItem[] users = new UserDbItem[30];
                _generalCrud = new GeneralCrud<UserDbItem>(connection);
                users = _generalCrud.ReadAllItems(_user);

                Assert.Equal(8, users.Length);
            }
        }

        [Fact]
        public void UpdateItem_should_work()
        {
            using (var connection = _connection)
            {
                connection.Open();
                _generalCrud = new GeneralCrud<UserDbItem>(connection);
                int result = _generalCrud.UpdateItem(_user, 11);

                Assert.Equal(1, result);
            }
        }

        [Fact]
        public void DeleteItem_should_work()
        {
            using (var connection = _connection)
            {
                connection.Open();
                _generalCrud = new GeneralCrud<UserDbItem>(connection);
                int result = _generalCrud.DeleteItem(_user, 11);

                Assert.Equal(1, result);
            }
        }
    }
}
