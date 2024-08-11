namespace NoteSystemWeb.Controllers
{
    public class AccountController(GeneralCrud crud) : Controller
    {
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {   
            PasswordHasher<UserDbItem> passwordHasher = new PasswordHasher<UserDbItem>();
            UserDbItem user = new UserDbItem();
            user = crud.ReadItem(user, login.Username);
            var hash = (int)passwordHasher.VerifyHashedPassword(user, user.UserHash, login.Password);

            if (user.UserName.Equals(login.Username) && hash == 1)
            {
                return RedirectToAction("Index", "Home");
            }

            throw new Exception("incorect credential");
        }

        [HttpPost]
        public IActionResult SignUp(SignUpModel signUp)
        {
            PasswordHasher<UserDbItem> passwordHasher = new PasswordHasher<UserDbItem>();
            UserDbItem user = new UserDbItem();
            string hash = passwordHasher.HashPassword(user, signUp.Password);
            user = new UserDbItem(signUp.Username, hash);
            crud.CreateItem(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpGet]        
        public IActionResult SignUp()
        {
            var model = new SignUpModel();
            return View(model); 
        } 

        public IActionResult Index()
        {
            return View();
        }
    }
}
