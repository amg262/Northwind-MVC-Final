using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr)
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
        }
        public IActionResult Login(string returnUrl)
        {
            // return url remembers the user's original request
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public ViewResult AccessDenied() => View();
        public IActionResult EmailLink(string returnUrl){
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailLink(EmailLinkModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                 AppUser user = await _userManager.FindByEmailAsync(details.Email);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(EmailLinkModel.Email), "Invalid Email");
                }
                
            }
        return View(details);
        }
         public IActionResult NewPassword(string returnUrl){
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> NewPassword(NewPasswordModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(details.Password);
                // NewPasswordModel password = new NewPasswordModel();
                if (details.Password == details.PasswordAgain)
                {
                    
                }
                else{
                    ModelState.AddModelError(nameof(NewPasswordModel.PasswordAgain), "Invalid Password");
                }
                
            }
        return View(details);
        }
    }
}