using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            var errorMessage = TempData["ErrorMessage"] as string;
            ViewBag.ErrorMessage = errorMessage ?? "An unexpected error occurred.";
            return View();
        }
    }
}
