using Party_Management.DTOs;
using Party_Management.Models;

namespace Party_Management.DTOs
{
    public class ProductResponseDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }
    }
}

public static class ProductExtension
{
    public static ProductResponseDTO ToProductResponse(this Product product, ProductRate productRate)
    {
        return new ProductResponseDTO()
        {
            ProductName = product.Name,
            ProductId = product.ProductId,
            Price = productRate.Rate
        };
    }
}
