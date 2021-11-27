using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AccountController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginPageViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email,
                model.Password,
                model.RememberMe,
                false);

            if (result.Succeeded)
            {
                if (Request.Query.Keys.Contains("ReturnUrl"))
                    return Redirect(Request.Query["ReturnUrl"].First());
                else
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Failed to Log in");
        }
        return View();
    }
}
