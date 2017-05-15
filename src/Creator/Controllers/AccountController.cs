using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Creator.Models;
using Microsoft.AspNetCore.Identity;
using Creator.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Creator.Controllers
{
    public class AccountController : Controller
    {

        private readonly CreatorDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        ///Use Dependency Injection to connect with the database and instantiate the user manager and sign-in manager. 
        ///UserManager works with the User table and SignInManager authenticates sign-in/sign-out
        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CreatorDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }


        /// GET: Index of AccountController - Returns the 'Index' page which will allow you to Register an account, login or logout
        public IActionResult Index()
        {
            return View();
        }

        /// GET: Register of AccountController
        public IActionResult Register()
        {
            return View();
        }

        /// POST: Register of AccountController - Take information from the Submitted RegisterViewModel ('model') and use it to create a new ApplicationUser via Identity
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        /// GET: Login of AccountController
        public IActionResult Login()
        {
            return View();
        }
        /// POST: Login - Take information from the submitted LoginViewModel and send it to the Identity SignInManager. If the Username and Password are correct the page will redirect to the Index page. The page will reload if the incorrect information is added.
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        ///POST: Log off of AccountController - Send SignInManager the command to sign out of the current user's account 
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
