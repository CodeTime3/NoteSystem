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
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), 
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            throw new Exception("incorect credential");
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

            return RedirectToAction("Index", "Home");
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
