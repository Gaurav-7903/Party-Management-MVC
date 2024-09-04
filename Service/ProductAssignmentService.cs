using Microsoft.EntityFrameworkCore;
using Party_Management.Data;
using Party_Management.DTOs;
using Party_Management.Models;
using Party_Management.ServiceContract;
using ServiceContract;
using System.Linq;

namespace Party_Management.Service
{
    public class ProductAssignmentService : IProductAssignmentService
    {

        private readonly ApplicationDbContext _db;
        private readonly IProductService _productService;
        private readonly IPartyService _partyService;

        public ProductAssignmentService(ApplicationDbContext db, IProductService productService, IPartyService partyService)
        {
            _db = db;
            _productService = productService;
            _partyService = partyService;
        }

        public ProductAssignResponse AssignProductToParty(int partyId, int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentNullException(nameof(productId));
            }
            if (partyId <= 0)
            {
                throw new ArgumentException(nameof(partyId));
            }

            PartyAssignment partyAssignment = new PartyAssignment()
            {
                ProductId = productId,
                PartyId = partyId,
                PartyAssignmentDate = DateTime.Now
            };

            _db.PartyAssignments.Add(partyAssignment);
            _db.SaveChanges();

            return new ProductAssignResponse()
            {
                ProductAssignId = partyAssignment.ProductId,
                ProductId = productId,
                PartyID = partyId
            };
        }

        public IEnumerable<ProductResponseDTO> GetAssignProductByPartyID(int partyId)
        {
            var party = _db.Parties.Find(partyId);
            if (party == null)
            {
                throw new Exception("Invalid Party Id");
            }

            var partyProduct = _db.PartyAssignments
                .Where(pa => pa.PartyId == partyId)
                .Include(pa => pa.Product)
                .Select(pa => new
                {
                    Product = pa.Product,
                    ProductRate = pa.Product.ProductRates
                        .Where(pr => pr.EffectiveDate <= DateTime.Now)
                        .OrderByDescending(pr => pr.EffectiveDate)
                        .FirstOrDefault()
                })
                .AsEnumerable()
                .Select(p => p.Product.ToProductResponse(p.ProductRate));

            return partyProduct;
        }

        public IEnumerable<ProductResponseDTO> GetNotAssignedProduct(int partyId)
        {
            var party = _db.Parties.Find(partyId);
            if (party == null)
            {
                throw new Exception("Invalid Party Id");
            }

            var asssignProcuts = _db.PartyAssignments
                .Where(p => p.PartyId == partyId)
                .Select(p => p.ProductId).ToList();


            var notPartyProduct = _db.Products
               .Where(pa => !asssignProcuts.Contains(pa.ProductId))
               .Select(pa => new
               {
                   Product = pa,
                   ProductRate = pa.ProductRates
                       .Where(pr => pr.EffectiveDate <= DateTime.Now)
                       .OrderByDescending(pr => pr.EffectiveDate)
                       .FirstOrDefault()
               })
               .AsEnumerable()
               .Select(p => p.Product.ToProductResponse(p.ProductRate));

            return notPartyProduct;
        }

        public IEnumerable<ProductAssignResponse> GetAllAssignProductAndPArty()
        {
            var productAndPartyData = _db.PartyAssignments.Include(p => p.Product).Include(p => p.Party).Select(p => new ProductAssignResponse
            {
                ProductId = p.Product.ProductId,
                ProductName = p.Product.Name,
                PartyID = p.Party.PartyId,
                PartyName = p.Party.PartyName,
                ProductAssignId = p.PartyAssignmentId
            });

            return productAndPartyData;
        }

    }
}
