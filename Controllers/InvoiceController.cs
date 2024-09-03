﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Party_Management.ServiceContract;
using Party_Management.ViewModels;
using ServiceContract;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IProductAssignmentService _productAssignmentService;
        private readonly IPartyService _partyService;

        public InvoiceController(IInvoiceService invoiceService, IProductAssignmentService productAssignmentService, IPartyService partyService)
        {
            _invoiceService = invoiceService;
            _productAssignmentService = productAssignmentService;
            _partyService = partyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int partyId)
        {
            var products = _productAssignmentService.GetAssignProductByPartyID(partyId);
            ViewBag.products = products.Select(p => new
            {
                ProductName = p.ProductName,
                ProductId = p.ProductId.ToString(),
                Price = p.Price.ToString("F2") // Format price as needed
            });
            InvoiceViewModel invoiceView = new InvoiceViewModel()
            {
                PartyId = partyId,
                PartyName = _partyService.GetPartyById(partyId).PartyName
            };
            return View(invoiceView);
        }
    }
}
