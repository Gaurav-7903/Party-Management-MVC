using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Party_Management.DTOs;
using Party_Management.ServiceContract;
using ServiceContract;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductAssignment : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductAssignmentService _productAssignmentService;

        public ProductAssignment(IProductService productService, IProductAssignmentService productAssignmentService)
        {
            _productService = productService;
            _productAssignmentService = productAssignmentService;
        }

        [HttpGet]
        public ActionResult ProductAssign(int partyId)
        {
            IEnumerable<ProductResponseDTO> products = _productAssignmentService.GetNotAssignedProduct(partyId);
            ViewBag.Products = products.Select(p =>
             new SelectListItem
             {
                 Text = p.ProductName,
                 Value = p.ProductId.ToString()
             });
            ProductAssignmentRequest productAssignmentRequest = new ProductAssignmentRequest()
            {
                PartyID = partyId
            };
            return View(productAssignmentRequest);
        }

        [HttpPost]
        public IActionResult ProductAssign(ProductAssignmentRequest request)
        {
            if (request == null)
            {
                return RedirectToAction(nameof(PartyController.Index), "Party");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                Console.WriteLine("Error occure");
                return View();
            }

            _productAssignmentService.AssignProductToParty(request.PartyID, request.SelectedProductId);

            return RedirectToAction(nameof(PartyController.Index), "Party");
        }

        [HttpGet]
        public IActionResult ProductAssignmentList()
        {
            IEnumerable<ProductAssignResponse> ProductAndPartyAssignData = _productAssignmentService.GetAllAssignProductAndPArty();
            return View(ProductAndPartyAssignData);
        }
    }
}
