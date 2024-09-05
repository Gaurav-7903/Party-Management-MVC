using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Party_Management.DTOs;
using Party_Management.Models;
using Party_Management.ServiceContract;
using ServiceContract;
using Services;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductRateService _productRateService;
        public ProductController(IProductService productService, IProductRateService productRateService)
        {
            _productService = productService;
            _productRateService = productRateService;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductResponseDTO> products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductRequestDTO productRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(productRequest);
            }

            ProductResponseDTO response = _productService.CreateProduct(productRequest);

            return RedirectToAction(nameof(ProductController.Index), "Product");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int productId)
        {
            ProductResponseDTO reponse = _productService.GetProductById(productId);
            return View(reponse);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(ProductResponseDTO productResponseDTO)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(productResponseDTO);
            }

            ProductResponseDTO response = _productService.UpdateProduct(productResponseDTO);

            return RedirectToAction(nameof(ProductController.Index), "Product");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int productId)
        {
            ProductResponseDTO reponse = _productService.GetProductById(productId);
            return View(reponse);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int ProductId, ProductResponseDTO productResponseDTO)
        {
            if (ProductId <= 0)
            {
                return RedirectToAction(nameof(ProductController.Index), "Product");
            }
            _productService.DeleteProduct(ProductId);
            return RedirectToAction(nameof(ProductController.Index), "Product");
        }

        [HttpGet]
        public IActionResult Details(int productId)
        {
            if (productId <= 0)
            {
                return RedirectToAction(nameof(PartyController.Index), "Product");
            }
            ProductResponseDTO product = _productService.GetProductById(productId);
            IEnumerable<ProductRateResponseDTO> productRate = _productRateService.GetProductRateById(productId);

            ProductRateAndProductDTO productRateAndProductDTO = new ProductRateAndProductDTO()
            {
                ProductResponseDTO = product,
                productRateResponseDTO = productRate,
            };
            return View(productRateAndProductDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeRate(ProductRateAndProductDTO request)
        {
            if (request.ProductRateRequestDTO == null)
            {
                return RedirectToAction(nameof(ProductController.Index), "Product");
            }
            ProductRateResponseDTO productRateResponse = _productRateService.ChangeProductRate(request.ProductRateRequestDTO);
            return RedirectToAction(nameof(ProductController.Details), "Product", new { productId = request.ProductRateRequestDTO.ProductId });
        }
    }
}

