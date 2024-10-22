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
            
            if(user.UserName is null || user.UserHash is null)
            {
                if(login.IsValid)
                {
                    login.IsValid = false;
                }

                return View(login);
            }

            var hash = passwordHasher.VerifyHashedPassword(user, user.UserHash, login.Password);

            if (user.UserName.Equals(login.Username) && hash is PasswordVerificationResult.Success)
            {   
                var claims = new List<Claim>
                {   
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), 
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            if(login.IsValid)
            {
                login.IsValid = false;
            }

            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUp)
        {
            PasswordHasher<UserDbItem> passwordHasher = new PasswordHasher<UserDbItem>();
            UserDbItem user = new UserDbItem();
            string hash = passwordHasher.HashPassword(user, signUp.Password);
            var users = crud.ReadAllItems(user);
            foreach (var usr in users)
            {
                if(usr.UserName.Equals(signUp.Username))
                {   
                    if(signUp.IsValid)
                    {
                        signUp.IsValid = false;
                    }
                    return View(signUp);
                }
            }
            user = new UserDbItem(signUp.Username, hash);
            crud.CreateItem(user);
            user = crud.ReadItem(user, signUp.Username);
            
            var claims = new List<Claim>
            {   
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);

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
            if(userIdString is null)
            {
                return RedirectToAction("Login");
            }
            var userId = int.Parse(userIdString);

            NoteDbItem note = new NoteDbItem(); 
            crud.DeleteItems(note, userId);

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
