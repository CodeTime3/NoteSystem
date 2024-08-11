namespace NoteSystemWeb.Controllers
{
    public class HomeController(GeneralCrud crud) : Controller
    {   
        public IActionResult Index()
        {
            List<NoteDbItem> list = new List<NoteDbItem>();
            return View();
        }
    }
}
