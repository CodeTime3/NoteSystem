namespace NoteSystemTest
{
    public class UserCrudTest
    {
        private static readonly string _connectionString = "server=localhost;uid=root;pwd=CT22d03p06;database=notesystem";
        private readonly MySqlConnection _connection = new MySqlConnection(_connectionString);
        private ICreateUpdateDelete<UserDbItem> _userItem;

        [Theory]
        [InlineData("pippo", "ciao")]
        [InlineData("pluto", "ciaone")]
        public void AddNewUser_should_work(string userName, string userHash)
        {
            var user = new UserDbItem { UserId = 1, UserName = userName, UserHash = userHash };

            using (var connection = _connection)
            {
                _connection.Open();
                _userItem = new UserCrud(_connection);
                _userItem.AddNewItem(user);

                Assert.Equal(userName, user.UserName);
                Assert.Equal(userHash, user.UserHash);
            }
        }

        [Theory]
        [InlineData("xhx", "pippo", "pippo")]
        [InlineData("kkk", "kkkkk", "kkkkk")]
        public void UpdateUserName_should_work(string firstName, string userName, string expected)
        {
            var user = new UserDbItem { UserId = 1, UserName = firstName, UserHash = "ciao" };

            using (var connection = _connection)
            {
                _connection.Open();
                _userItem = new UserCrud(_connection);
                user.UserName = userName;
                _userItem.UpdateItem(user);

                Assert.Equal(expected, user.UserName);
            }
        }

        [Theory]
        [InlineData("jjjj", "ciao", "ciao")]
        [InlineData("jjjj", "dani", "dani")]
        public void UpdateUserHash_should_work(string firstHash, string userHash, string expected)
        {
            var user = new UserDbItem { UserId = 1, UserName = "pippo ", UserHash = firstHash };

            using (var connection = _connection)
            {
                _connection.Open();
                _userItem = new UserCrud(_connection);
                user.UserHash = userHash;
                _userItem.UpdateItem(user);

                Assert.Equal(expected, user.UserHash);
            }
        }

        [Fact]
        public void DeleteUser_should_work()
        {
            var user = new UserDbItem { UserId = 1, UserName = "ciao", UserHash = "ciao" };

            using (var connection = _connection)
            {
                _connection.Open();
                _userItem = new UserCrud(_connection);
                int result = _userItem.DeleteItem(user.UserId);

                Assert.Equal(1, result);
            }
        }
    }
}