namespace NoteSystemWeb.Controllers
{   
    [Authorize]
    public class HomeController(IGeneralCrud crud) : Controller
    {   
        [HttpPost]
        public IActionResult CreateNote(string noteTitle, string noteText)
        {
            var userId = GetUserId();

			NoteDbItem note = new NoteDbItem(userId, noteTitle, noteText);
            crud.CreateItem(note);
            note = crud.ReadItem(note, userId);

            return Json(note);
        }

        [HttpGet]
        public IActionResult ReadNotes()
        {   
            NoteDbItem note = new NoteDbItem();

            var model = new NoteModel
            {
                Notes = crud.ReadItems(note, GetUserId())
            };

            return Json(model);
        }

        [HttpPut]
        public IActionResult UpdateNote(int noteId, string noteTitle, string noteText)
        {   
            NoteDbItem note = new NoteDbItem(noteId, GetUserId(), noteTitle, noteText);
            crud.UpdateItem(note, noteId);

            return Json(note);
        }

        [HttpDelete]
        public IActionResult DeleteNote(int noteId)
        {   
            NoteDbItem note = new NoteDbItem();
            crud.DeleteItem(note, noteId);

            return Json(noteId);
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

        private int GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);

            return userId;
        }
    }
}