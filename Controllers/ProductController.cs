using Microsoft.AspNetCore.Mvc;
using Party_Management.DTOs;
using Party_Management.Models;
using ServiceContract;
using Services;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
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
        public IActionResult Update(int productId)
        {
            ProductResponseDTO reponse = _productService.GetProductById(productId);
            return View(reponse);
        }

        [HttpPost]
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
        public IActionResult Delete(int productId)
        {
            ProductResponseDTO reponse = _productService.GetProductById(productId);
            return View(reponse);
        }

        [HttpPost]
        public IActionResult Delete(int ProductId, ProductResponseDTO productResponseDTO)
        {
            if (ProductId <= 0)
            {
                return RedirectToAction(nameof(PartyController.Index), "Product");
            }
            _productService.DeleteProduct(ProductId);
            return RedirectToAction(nameof(PartyController.Index), "Product");
        }
    }
}

