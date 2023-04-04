using CodeParts.Data;
using CodeParts.Models;
using CodeParts.Service.Implementations;
using CodeParts.Service.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeParts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("AllCodePage", "Code");
        }
        public async Task<IActionResult> ReloadDB()
        {
            HttpContext.RequestServices.GetService<AppDbContext>().UpdateDatabase();
            return RedirectToAction("AllCodePage", "Code");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}