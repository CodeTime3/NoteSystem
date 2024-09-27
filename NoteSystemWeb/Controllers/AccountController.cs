namespace NoteSystemWeb.Controllers
{
    public class AccountController(GeneralCrud crud) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {   
            PasswordHasher<UserDbItem> passwordHasher = new PasswordHasher<UserDbItem>();
            UserDbItem user = new UserDbItem();
            user = crud.ReadItem(user, login.Username);
            var hash = (int)passwordHasher.VerifyHashedPassword(user, user.UserHash, login.Password);

            if (user.UserName.Equals(login.Username) && hash == 1)
            {
                var claims = new List<Claim>
                {   
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), 
                    authProperties);

                HttpContext.Session.SetString("UserId", user.UserId.ToString());

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUp)
        {
            PasswordHasher<UserDbItem> passwordHasher = new PasswordHasher<UserDbItem>();
            UserDbItem user = new UserDbItem();
            string hash = passwordHasher.HashPassword(user, signUp.Password);
            user = new UserDbItem(signUp.Username, hash);
            crud.CreateItem(user);
            
            var claims = new List<Claim>
            {   
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);

            HttpContext.Session.SetString("UserId", user.UserId.ToString());

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> DeleteAccount()
        {   
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);

            //ToDo: prima cancellare tutte le note e poi l'utente nello stesso metodo 

            UserDbItem user = new UserDbItem();
            crud.DeleteItem(user, userId);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpGet]        
        public async Task<IActionResult> SignUp()
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
