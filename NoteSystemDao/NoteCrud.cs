namespace NoteSystemDao
{
    public class NoteCrud : ICreateUpdateDelete<NoteDbItem>, ISelectItems<NoteDbItem>
    {
        private readonly MySqlConnection _connection;
        public NoteCrud(MySqlConnection connection)
        {
            _connection = connection;
        }

        public int AddNewItem(NoteDbItem note)
        {
            var userId = GetUserId();
            string cmdTxt = $@"insert into notes (user_id, note_title, note_text) values (@user_id, @note_title, @note_text)";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@note_title", note.NoteText);
                cmd.Parameters.AddWithValue("@note_text", note.NoteTitle);

                return cmd.ExecuteNonQuery();
            }
        }

        private int GetUserId()
        {
            return 1;
        }

        public NoteDbItem[] GetAllItems() =>
            GetItems(" ");

        public NoteDbItem[] GetItems(string strtTitle)
        {
            var notes = new List<NoteDbItem>();
            var cmdTxt = $@"select note_title from notes where note_title LIKE BINARY @note_title";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                string searchTerm = "%" + strtTitle + "%";
                cmd.Parameters.AddWithValue("@note_title", searchTerm);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var note = new NoteDbItem
                        {
                            NoteTitle = reader.GetString(0)
                        };

                        notes.Add(note);
                    }
                }
            }

            return notes.ToArray();
        }

        public int UpdateItem(NoteDbItem note)
        {
            var userId = GetUserId();
            var cmdTxt = $@"update notes set note_title = @note_title, note_text = @note_text where note_id = @note_id";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@note_id", note.NoteId);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@note_title", note.NoteTitle);
                cmd.Parameters.AddWithValue("@note_text", note.NoteText);

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteItem(int id)
        {
            var cmdTxt = $@"delete from notes where note_id = @note_id";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@note_id", id);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}