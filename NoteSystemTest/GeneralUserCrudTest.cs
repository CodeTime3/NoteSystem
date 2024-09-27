namespace NoteSystemTest
{
    public class GeneralUserCrudTest
    {
        private static readonly string _connectionString = "server=localhost;uid=root;pwd=CT22d03p06;database=notesystem";
        private readonly MySqlConnection _connection = new MySqlConnection(_connectionString);
        private GeneralCrud _generalCrud;
        private UserDbItem _user = new UserDbItem { UserName = "pluto2", UserHash = "pippo25" };
        private NoteDbItem _note = new NoteDbItem { UserId = 3, NoteText = "h", NoteTitle = "h" };

        [Fact]
        public void CreateItem_should_work()
        {
            using (var connection = _connection)
            {   
                connection.Open();
                _generalCrud = new GeneralCrud(connection);
                int result = _generalCrud.CreateItem(_note);

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
                _generalCrud = new GeneralCrud(connection);
                users = _generalCrud.ReadAllItems(_user);

                Assert.Equal(7, users.Length);
            }
        }

        [Fact]
        public void GetItem_should_work()
        {
            using (var connection = _connection)
            {
                connection.Open();
                UserDbItem user = new UserDbItem();
                List<UserDbItem> users = new List<UserDbItem>();
                _generalCrud = new GeneralCrud(connection);
                user = _generalCrud.ReadItem(_user, "pippo");
                users.Add(user); 

                Assert.Equal(1, users.Count);
            }
        }

        [Fact]
        public void GetItem_should_work2()
        {
            using (var connection = _connection)
            {
                connection.Open();
                NoteDbItem note = new NoteDbItem();
                List<NoteDbItem> notes = new List<NoteDbItem>();
                _generalCrud = new GeneralCrud(connection);
                note = _generalCrud.ReadItem(note, 5);
                notes.Add(note); 

                Assert.Equal(1, notes.Count);
            }
        }

        [Fact]
        public void ReadItems_should_work()
        {
            using (var connection = _connection)
            {
                connection.Open();
                NoteDbItem note = new NoteDbItem();
                NoteDbItem[] notes = new NoteDbItem[30];
                _generalCrud = new GeneralCrud(connection);
                notes = _generalCrud.ReadItems(note, 3);

                Assert.Equal(2, notes.Length);
            }
        }

        [Fact]
        public void UpdateItem_should_work()
        {
            using (var connection = _connection)
            {   
                NoteDbItem note = new NoteDbItem(5, "danihh", null);
                connection.Open();
                _generalCrud = new GeneralCrud(connection);
                int result = _generalCrud.UpdateItem(note, 13);

                Assert.Equal(1, result);
            }
        }

        [Fact]
        public void DeleteItem_should_work()
        {
            using (var connection = _connection)
            {
                connection.Open();
                _generalCrud = new GeneralCrud(connection);
                int result = _generalCrud.DeleteItem(_user, 6);

                Assert.Equal(1, result);
            }
        }

        [Fact]
        public void DeleteItems_should_work()
        {
            using (var connection = _connection)
            {
                connection.Open();
                _generalCrud = new GeneralCrud(connection);
                int result = _generalCrud.DeleteItems(_note, 9);

                Assert.Equal(1, result);
            }
        }
    }
}
