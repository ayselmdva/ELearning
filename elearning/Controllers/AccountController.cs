using elearning.Models;
using elearning.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace elearning.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _user;
        private readonly SignInManager<AppUser> _signIn;

        public AccountController(UserManager<AppUser> user, SignInManager<AppUser> signIn)
        {
            _user = user;
            _signIn = signIn;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.UserName
            };

            var result=await _user.CreateAsync(user,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (loginVM.UserNameOrUser.Contains("@"))
            {
                var user=await _user.FindByEmailAsync(loginVM.UserNameOrUser);
                if (user == null) { return NotFound(); }
                var result = await _signIn.PasswordSignInAsync(user, loginVM.Password, true, true);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
            }
            else
            {
                var user = await _user.FindByNameAsync(loginVM.UserNameOrUser);
                if (user == null) { return NotFound(); }
                var result = await _signIn.PasswordSignInAsync(user, loginVM.Password, true, true);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
            }


            return RedirectToAction("Index", "Home");
        }
    }
}
