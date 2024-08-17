namespace NoteSystemWeb.Controllers
{
    public class HomeController(GeneralCrud crud) : Controller
    {   
        [HttpPost]
        public IActionResult CreateNote(NoteModel noteModel)
        {   
            noteModel.UserId = 3;
            crud.CreateItem(noteModel);

            return RedirectToAction("Index", "Home");
        }
 
        [HttpPost]
        public IActionResult UpdateNote(NoteModel noteModel)
        {   
            noteModel.UserId = 3;
            crud.UpdateItem(noteModel, noteModel.UserId);
            return RedirectToAction("Index", "Home");
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
