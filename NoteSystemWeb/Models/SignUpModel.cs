namespace NoteSystemWeb.Models
{
	public class SignUpModel
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public bool IsValid { get; set; } = true;
	}
}
