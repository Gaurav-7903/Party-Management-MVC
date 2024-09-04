using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Party_Management.DTOs;
using Party_Management.ServiceContract;
using Party_Management.ViewModels;
using ServiceContract;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    //[IgnoreAntiforgeryToken]
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
            IEnumerable<InvoiceResponseDTO> allInvoices = _invoiceService.GetAllInvoice();
            return View(allInvoices);
        }

        [HttpGet]
        public IActionResult Create(int? partyId)
        {
            ViewBag.Party = _partyService.GetAllParty().Select(p => new
            {
                Text = p.PartyName,
                Value = p.PartyId.ToString()
            });
            InvoiceViewModel invoiceView;

            if (partyId.HasValue)
            {
                var products = _productAssignmentService.GetAssignProductByPartyID(partyId.Value);
                ViewBag.products = products.Select(p => new
                {
                    ProductName = p.ProductName,
                    ProductId = p.ProductId.ToString(),
                    Price = p.Price.ToString("F2") // Format price as needed
                });
                invoiceView = new InvoiceViewModel()
                {
                    PartyId = partyId.Value,
                    PartyName = _partyService.GetPartyById(partyId.Value).PartyName
                };
            }
            else
            {
                invoiceView = new InvoiceViewModel();
                ViewBag.Products = Enumerable.Empty<SelectListItem>();
            }
            return View(invoiceView);
        }


        [HttpPost]
        public IActionResult CreateInvoice([FromBody] List<InvoiceRequestDTO> invoiceData)
        {
            Console.WriteLine("Enter in Controller");
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid invoice data. ");
            }
            Console.WriteLine(invoiceData);
            if (invoiceData == null || invoiceData.Count() == 0)
            {
                Console.WriteLine(invoiceData.Count());
                return BadRequest("Invalid invoice data. ");
            }

            InvoiceResponseDTO invoiceResponse = _invoiceService.AddInvoice(invoiceData, invoiceData[0].PartyId);

            return Ok(new { Message = "Invoice created successfully!", Total = invoiceResponse.Total.ToString(), TotalItem = invoiceResponse.ProductCount.ToString() });
        }

        [HttpGet]
        public IActionResult Details(int invoiceId)
        {
            if (invoiceId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(invoiceId));
            }

            InvoiceDetailDTO invoiceDetail = _invoiceService.GetInvoiceDetailsByInvoiceId(invoiceId);

            return View(invoiceDetail);
        }

    }
}
