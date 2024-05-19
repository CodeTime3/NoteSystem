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
            string cmdTxt = $@"insert into notes (UserId, NoteTitle, NoteText) values (@UserId, @NoteTitle, @NoteText)";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@NoteTitle", note.NoteText);
                cmd.Parameters.AddWithValue("@NoteText", note.NoteTitle);

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
            var cmdTxt = $@"select NoteTitle from notes where NoteTitle LIKE BINARY @NoteTitle";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                string searchTerm = "%" + strtTitle + "%";
                cmd.Parameters.AddWithValue("@NoteTitle", searchTerm);

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
            var cmdTxt = $@"update notes set NoteTitle = @NoteTitle, NoteText = @NoteText where NoteId = @NoteId";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@NoteId", note.NoteId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@NoteTitle", note.NoteTitle);
                cmd.Parameters.AddWithValue("@NoteText", note.NoteText);

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteItem(int id)
        {
            var cmdTxt = $@"delete from notes where NoteId = @NoteId";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
                cmd.Parameters.AddWithValue("@NoteId", id);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}