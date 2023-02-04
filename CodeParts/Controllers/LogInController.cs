using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeParts.Controllers
{
    public class LogInController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
