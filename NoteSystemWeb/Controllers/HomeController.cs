namespace NoteSystemWeb.Controllers
{
    public class HomeController(GeneralCrud crud) : Controller
    {   
        [HttpPost]
        public IActionResult CreateNote(string noteTitle, string noteText)
        {
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);

			NoteDbItem note = new NoteDbItem(userId, noteTitle, noteText);
            crud.CreateItem(note);

            return Json( new {success = true, messagge = "added note"});
        }

        [HttpGet]
        public IActionResult ReadNotes()
        {   
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);

            NoteDbItem note = new NoteDbItem();

            var model = new NoteModel
            {
                Notes = crud.ReadItems(note, userId)
            };

            return Json(model);
        }

        [HttpPut]
        public IActionResult UpdateNote(int noteId, string noteTitle, string noteText)
        {   
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);

            NoteDbItem note = new NoteDbItem(noteId, userId, noteTitle, noteText);
            crud.UpdateItem(note, noteId);

            return Json( new {success = true});
        }

        [HttpDelete]
        public IActionResult DeleteNote(int noteId)
        {   
            NoteDbItem note = new NoteDbItem();
            crud.DeleteItem(note, noteId);

            return Json( new {success = true});
        }

        [HttpGet]
        public IActionResult CreateNote()
        {
            var model = new NoteModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateNote()
        {   
            var model = new NoteModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteNote()
        {
            var model = new NoteModel();
            return View(model);
        }

        public IActionResult Index()
        {   
            return View();
        }
    }
}