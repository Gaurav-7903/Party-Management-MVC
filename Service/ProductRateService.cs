using Party_Management.Data;
using Party_Management.DTOs;
using Party_Management.Models;
using Party_Management.ServiceContract;

namespace Party_Management.Service
{
    public class ProductRateService : IProductRateService
    {
        private readonly ApplicationDbContext _db;

        public ProductRateService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ProductRateResponseDTO> GetProductRate()
        {
            IEnumerable<ProductRateResponseDTO> productRate = _db.ProductRates.Select(pr => new ProductRateResponseDTO
            {
                productId = pr.ProductId,
                EffectiveDate = pr.EffectiveDate,
                Rate = pr.Rate,
                ProductRateId = pr.ProductId,
                ProductName = _db.Products.Where(p => p.ProductId == pr.ProductId).Select(p => p.Name).FirstOrDefault(),
            });

            return productRate;
        }

        public IEnumerable<ProductRateResponseDTO> GetProductRateById(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productId));
            }
            var product = _db.Products.Find(productId);
            var productRate = _db.ProductRates.Where(product => product.ProductId == productId).Select(p => p.ToProductRateResponse(product));

            return productRate;
        }

        public ProductRateResponseDTO ChangeProductRate(ProductRateRequestDTO productRateRequestDTO)
        {
            if (productRateRequestDTO == null)
            {
                throw new AggregateException(nameof(productRateRequestDTO));
            }

            var productRate = new ProductRate()
            {
                ProductId = productRateRequestDTO.ProductId,
                Rate = productRateRequestDTO.Rate,
                EffectiveDate = productRateRequestDTO.EffectiveDate,
            };
            var product = _db.Products.Find(productRateRequestDTO.ProductId);
            _db.ProductRates.Add(productRate);

            _db.SaveChanges();

            return productRate.ToProductRateResponse(product);
        }
    }
}
