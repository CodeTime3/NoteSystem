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
            string cmdTxt = $@"insert into users (user_name, user_hash) values (@user_name, @user_hash)";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@user_name", user.UserName);
                cmd.Parameters.AddWithValue("@user_hash", user.UserHash);

                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateItem(UserDbItem user)
        {
            var cmdTxt = $@"update users set user_name = @user_name, user_hash = @user_hash where user_id = @user_id";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@user_id", user.UserId);
                cmd.Parameters.AddWithValue("@user_name", user.UserName);
                cmd.Parameters.AddWithValue("@user_hash", user.UserHash);

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteItem(int id)
        {
            var cmdTxt = $@"delete from users where user_id = @user_id";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@user_id", id);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}