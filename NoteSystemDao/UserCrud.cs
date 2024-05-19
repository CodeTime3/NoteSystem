namespace NoteSystemDao
{
    public class UserCrud : ICreateUpdateDelete<UserDbItem>
    {
        private readonly MySqlConnection _connection;
        public UserCrud(MySqlConnection connection)
        {
            _connection = connection;
        }

        public int AddNewItem(UserDbItem user)
        {
            string cmdTxt = $@"insert into users (UserName, UserHash) values (@UserName, @UserHash)";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@UserHash", user.UserHash);

                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateItem(UserDbItem user)
        {
            var cmdTxt = $@"update users set UserName = @UserName, UserHash = @UserHash where UserId = @UserId";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@UserHash", user.UserHash);

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteItem(int id)
        {
            var cmdTxt = $@"delete from users where UserId = @UserId";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@UserId", id);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}