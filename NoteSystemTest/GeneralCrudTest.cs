namespace NoteSystemTest
{
    public class GeneralCrudTest
    {
        private static string _connectionString = "server=localhost;uid=root;pwd=CT22d03p06;database=notesystem";
        private static MySqlConnection _connection = new MySqlConnection(_connectionString);
        private static GetNameTableAttribute _getNameTable = new GetNameTableAttribute();
        private static StringQueriesBuilderParameter _queryBuilder = new StringQueriesBuilderParameter();
        private IGeneralCrud _generalCrud = new GeneralCrud(_connection, _getNameTable, _queryBuilder);
        private UserDbItem _user = new UserDbItem { UserName = "pluto2", UserHash = "pippo25" };
        private NoteDbItem _note = new NoteDbItem { UserId = 3, NoteText = "h", NoteTitle = "h" };

        [Fact]
        public void CreateItem_should_work()
        {
            int result = _generalCrud.CreateItem(_note);

            Assert.Equal(1, result);
        }

        [Fact]
        public void ReadAllItems_should_work()
        {
            UserDbItem[] users = new UserDbItem[30];
            users = _generalCrud.ReadAllItems(_user);

            Assert.Equal(2, users.Length);
        }

        [Fact]
        public void GetItem_should_work()
        {
            UserDbItem user = new UserDbItem();
            List<UserDbItem> users = new List<UserDbItem>();
            user = _generalCrud.ReadItem(user, "h");
            users.Add(user); 

            Assert.Equal(1, users.Count);
        }

        [Fact]
        public void GetItem_should_work2()
        {
            NoteDbItem note = new NoteDbItem();
            List<NoteDbItem> notes = new List<NoteDbItem>();
            note = _generalCrud.ReadItem(note, 3);
            notes.Add(note); 

            Assert.Equal(1, notes.Count);
        }

        [Fact]
        public void ReadItems_should_work()
        {
            NoteDbItem note = new NoteDbItem();
            NoteDbItem[] notes = new NoteDbItem[30];
            notes = _generalCrud.ReadItems(note, 3);

            Assert.Equal(1, notes.Length);
        }

        [Fact]
        public void UpdateItem_should_work()
        {
            NoteDbItem note = new NoteDbItem(3, "danihh", "ppp");
            int result = _generalCrud.UpdateItem(note, 18);

            Assert.Equal(1, result);
        }

        [Fact]
        public void DeleteItem_should_work()
        {
            int result = _generalCrud.DeleteItem(_user, 3);

            Assert.Equal(1, result);
        }

        [Fact]
        public void DeleteItems_should_work()
        {
            int result = _generalCrud.DeleteItems(_note, 3);

            Assert.Equal(1, result);
        }
    }
}