﻿using Party_Management.Data;
using Party_Management.DTOs;
using Party_Management.Models;
using ServiceContract;


namespace Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ProductResponseDTO CreateProduct(ProductRequestDTO productRequestDTO)
        {

            if (productRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(productRequestDTO));
            }

            Product product = new Product()
            {
                Name = productRequestDTO.ProuctName
            };
            _db.Products.Add(product);
            _db.SaveChanges();

            var productRate = new ProductRate
            {
                ProductId = product.ProductId,
                Rate = productRequestDTO.Price,
                EffectiveDate = DateTime.Now
            };

            _db.ProductRates.Add(productRate);
            _db.SaveChanges();

            return product.ToProductResponse(productRate);
        }

        public ProductResponseDTO UpdateProduct(ProductResponseDTO productResponseDTO)
        {
            if (productResponseDTO == null)
            {
                throw new ArgumentNullException(nameof(productResponseDTO));
            }

            var product = _db.Products.Find(productResponseDTO.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            product.Name = productResponseDTO.ProductName;
            _db.SaveChanges();
            return productResponseDTO;
        }

        public IEnumerable<ProductResponseDTO> GetAllProducts()
        {
            var products = _db.Products.Select(p => new
            {
                p.ProductId,
                p.Name,
                Rate = _db.ProductRates
                    .Where(pr => pr.ProductId == p.ProductId)
                    .OrderByDescending(pr => pr.EffectiveDate)
                    .Select(pr => pr.Rate)
                    .FirstOrDefault()
            }).ToList();

            var productResponseDTOs = products.Select(p => new ProductResponseDTO
            {
                ProductId = p.ProductId,
                ProductName = p.Name,
                Price = p.Rate
            });

            return productResponseDTOs;
        }

        public bool DeleteProduct(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productId));
            }
            var product = _db.Products.Find(productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product Not Found");
            }

            // Remove Price as well from ProductRate
            var productRates = _db.ProductRates.Where(pr => pr.ProductId == productId).ToList();
            _db.ProductRates.RemoveRange(productRates);

            _db.Products.Remove(product);

            _db.SaveChanges();

            return true;
        }

        public ProductResponseDTO GetProductById(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productId));
            }
            var product = _db.Products.Find(productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product Not Found");
            }

            ProductRate? productRate = _db.ProductRates.Where(p => p.ProductId == productId && p.EffectiveDate <= DateTime.Now).OrderByDescending(p => p.EffectiveDate).FirstOrDefault();

            return product.ToProductResponse(productRate);
        }




    }
}
