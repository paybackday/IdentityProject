using IdentityProject.Models.Authentication;
using IdentityProject.VMClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListUser() 
        {
            return View(_userManager.Users);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserVM appUserVM) 
        {
            if (ModelState.IsValid) //ModelState'de bir sorun yoksa islemleri yap.
            {
                AppUser appUser = new AppUser
                {
                    Email = appUserVM.Email,
                    UserName = appUserVM.UserName,
                };
                IdentityResult identityResult = await _userManager.CreateAsync(appUser,appUserVM.Sifre);
                if (identityResult.Succeeded)
                    return RedirectToAction("ListUser", "Auth");
            }
            return View(); //Varsa view i don ve hatalari bas.
        }
    }
}
