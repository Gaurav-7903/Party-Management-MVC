using Microsoft.AspNetCore.Mvc;
using Party_Management.DTOs;
using Party_Management.ServiceContract;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductRateController : Controller
    {
        private readonly IProductRateService _productRateService;


        public ProductRateController(IProductRateService productRateService)
        {
            _productRateService = productRateService;
        }

        public IActionResult List()
        {
            IEnumerable<ProductRateResponseDTO> producatRate = _productRateService.GetProductRate();
            return View(producatRate);
        }
    }
}
