using Microsoft.AspNetCore.Mvc;

namespace Party_Management.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
