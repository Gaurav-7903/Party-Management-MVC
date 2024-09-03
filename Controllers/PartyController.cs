using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Party_Management.DTOs;
using Party_Management.Models;
using Party_Management.ServiceContract;
using ServiceContract;
using Services;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    //[Authorize]
    public class PartyController : Controller
    {
        private readonly IPartyService _partyService;
        private readonly IProductAssignmentService _partyAssignmentService;
        private readonly IProductService _productService;
        public PartyController(IPartyService partyService, IProductAssignmentService partyAssignmentService, IProductService productService)
        {
            _partyService = partyService;
            _partyAssignmentService = partyAssignmentService;
            _productService = productService;
        }


        [Route("/")]
        public IActionResult Index()
        {
            IEnumerable<PartyResponseDTO> allParty = _partyService.GetAllParty();
            return View(allParty);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PartyRequestDTO partyRequest)
        {

            Console.WriteLine("Create a Party");

            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(partyRequest);
            }

            PartyResponseDTO respone = _partyService.CreateParty(partyRequest);

            Console.WriteLine("Party Created");

            return RedirectToAction(nameof(PartyController.Index), "Party");
        }

        [HttpGet]
        public IActionResult Update(int partyId)
        {

            PartyResponseDTO party = _partyService.GetPartyById(partyId);

            return View(party);
        }

        [HttpPost]
        public IActionResult Update(PartyResponseDTO partyRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(partyRequest);
            }
            PartyResponseDTO party = _partyService.UpdateParty(partyRequest);
            return RedirectToAction(nameof(PartyController.Index), "Party");
        }

        [HttpGet]
        public IActionResult Delete(int partyId)
        {
            PartyResponseDTO party = _partyService.GetPartyById(partyId);
            return View(party);
        }

        [HttpPost]
        public IActionResult Delete(int PartyId, PartyResponseDTO partyResponseDTO)
        {
            if (PartyId <= 0)
            {
                return RedirectToAction(nameof(PartyController.Index), "Party");
            }
            PartyResponseDTO party = _partyService.DeleteParty(PartyId);
            return RedirectToAction(nameof(PartyController.Index), "Party");
        }

        public IActionResult Details(int partyId)
        {
            if (partyId <= 0)
            {
                return RedirectToAction(nameof(PartyController.Index), "Party");
            }

            PartyResponseDTO partyResponseDTO = _partyService.GetPartyById(partyId);
            IEnumerable<ProductResponseDTO> productRequestDTO = _partyAssignmentService.GetAssignProductByPartyID(partyId);

            ProductAssignDTO productAssignDTO = new ProductAssignDTO()
            {
                PartyResponseDTO = partyResponseDTO,
                ProductResponseDTOs = productRequestDTO,
            };
            return View(productAssignDTO);
        }


    } // partyAssigment
}
