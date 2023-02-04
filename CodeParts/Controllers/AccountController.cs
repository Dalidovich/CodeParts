using CodeParts.Data.ViewModel.Account;
using CodeParts.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeParts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _accountService.Delete(User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAccountsCode()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _accountService.DeleteAllDataAccount(User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult> deleteConfirm(int codeCount)
        {
            return PartialView(codeCount);
        }


        [HttpGet]
        public ActionResult LogIn() => View();
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var responce = await _accountService.Authentication(model);
                if (responce.StatusCode == Data.enums.StatusCode.AccountRead)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                         new ClaimsPrincipal(responce.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", responce.Description);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Resgistration() => View();
        [HttpPost]
        public async Task<IActionResult> Resgistration(RegisterAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var responce = await _accountService.Registration(model);
                if (responce.StatusCode == Data.enums.StatusCode.AccountCreate)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(responce.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", responce.Description);
            }
            return View(model);
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Index(string login)
        {
            var responce = await _accountService.GetOneWithCode(login);
            if (responce.StatusCode == Data.enums.StatusCode.AccountRead)
            {
                return View(responce.Data);
            }
            return RedirectToAction("Error");
        }
    }
}
