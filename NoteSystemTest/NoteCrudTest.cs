namespace NoteSystemTest
{
    public class NoteCrudTest
    {
        private static readonly string _connectionString = "server=localhost;uid=root;pwd=CT22d03p06;database=notesystem";
        private readonly MySqlConnection _connection = new MySqlConnection(_connectionString);
        private ICreateUpdateDelete<NoteDbItem> _noteItems;
        private ISelectItems<NoteDbItem> _selectItems;

        [Theory]
        [InlineData("hh", "hh")]
        [InlineData("JJ", "JJ")]
        public void AddNewTitleNote_should_work(string title, string expected)
        {
            var note = new NoteDbItem { NoteId = 1, UserId = 1, NoteTitle = title, NoteText = "kkjwq" };

            using (var connection = _connection)
            {
                connection.Open();
                _noteItems = new NoteCrud(_connection);
                _noteItems.AddNewItem(note);

                Assert.Equal(expected, note.NoteTitle);
            }
        }

        [Theory]
        [InlineData("hh", "hh")]
        [InlineData("JJ", "JJ")]
        public void AddNewTextNote_should_work(string text, string expected)
        {
            var note = new NoteDbItem { NoteId = 1, UserId = 1, NoteTitle = "bhjbq", NoteText = text };

            using (var connection = _connection)
            {
                connection.Open();
                _noteItems = new NoteCrud(_connection);
                _noteItems.AddNewItem(note);

                Assert.Equal(expected, note.NoteText);
            }
        }

        [Theory]
        [InlineData("hh", "kkk", "kkk")]
        [InlineData("ii", "bbb", "bbb")]
        public void UpdateTitleNote_should_work(string firstTitle, string title, string expected)
        {
            var note = new NoteDbItem { NoteId = 1, UserId = 1, NoteTitle = firstTitle, NoteText = "hvcqhc" };

            using (var connection = _connection)
            {
                connection.Open();
                _noteItems = new NoteCrud(_connection);
                note.NoteTitle = title;
                _noteItems.UpdateItem(note);

                Assert.Equal(expected, note.NoteTitle);
            }
        }

        [Theory]
        [InlineData("hh", "kkk", "kkk")]
        [InlineData("ii", "bbb", "bbb")]
        public void UpdateTextNote_should_work(string firstText, string text, string expected)
        {
            var note = new NoteDbItem { NoteId = 1, UserId = 1, NoteTitle = "hscqhdv", NoteText = firstText };

            using (var connection = _connection)
            {
                connection.Open();
                _noteItems = new NoteCrud(_connection);
                note.NoteText = text;
                _noteItems.UpdateItem(note);

                Assert.Equal(expected, note.NoteText);
            }
        }

        [Fact]
        public void DeleteNote_should_work()
        {
            var note = new NoteDbItem { NoteId = 1, UserId = 1, NoteTitle = "hscqhdv", NoteText = "jvhfqj" };

            using (var connection = _connection)
            {
                _connection.Open();
                _noteItems = new NoteCrud(_connection);
                int result = _noteItems.DeleteItem(note.NoteId);

                Assert.Equal(0, result);
            }
        }

        [Fact]
        public void GetAllNotes_should_work()
        {
            using (var connection = _connection)
            {
                _connection.Open();
                _selectItems = new NoteCrud(_connection);
                var notes = _selectItems.GetAllItems();

                foreach (var note in notes)
                {
                    Assert.Equal(1, note.UserId);
                }
            }
        }

        [Fact]
        public void GetNotes_should_work()
        {
            using (var connection = _connection)
            {
                _connection.Open();
                _selectItems = new NoteCrud(_connection);
                var notes = _selectItems.GetItems("h");

                foreach (var note in notes)
                {
                    Assert.Equal("hh", note.NoteTitle);
                }
            }
        }
    }
}