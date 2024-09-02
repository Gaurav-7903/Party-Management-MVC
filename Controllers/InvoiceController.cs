using Microsoft.AspNetCore.Mvc;

namespace Party_Management.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
