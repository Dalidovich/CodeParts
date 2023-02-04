using CodeParts.Data.ViewModel.Code;
using CodeParts.Service.Implementations;
using CodeParts.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CodeParts.Data.TableModel;

namespace CodeParts.Controllers
{
    public class CodeController : Controller
    {

        private readonly ICodeService _codeService;

        public CodeController(ICodeService codeService)
        {
            _codeService = codeService;
        }

        [HttpGet]
        public async Task<IActionResult> AllCodePage()
        {
            var response = await _codeService.GetContent();
            if (response.StatusCode == Data.enums.StatusCode.CodeRead)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }
        public async Task<IActionResult> OneCodePage(long id)
        {
            var response = await _codeService.GetOne(id);
            if (response.StatusCode == Data.enums.StatusCode.CodeRead)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }


        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(CodeViewModel model,string tags)
        {
            model.userLogin = User.Identity.Name == null ? "anonim" : User.Identity.Name;
            model.tags= tags;
            if (ModelState.IsValid)
            {
                var responce = await _codeService.Create(model);
                if (responce.StatusCode == Data.enums.StatusCode.CodeCreate)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", responce.Description);
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(long id,string login)
        {
            if (User.Identity.Name == login)
            {
                await _codeService.Delete(id);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
