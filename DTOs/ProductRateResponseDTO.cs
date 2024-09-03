using Party_Management.DTOs;
using Party_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Party_Management.DTOs
{

    public class ProductRateResponseDTO
    {
        public int ProductRateId { get; set; }

        public int productId { get; set; }

        public string ProductName { get; set; }

        public decimal Rate { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}

public static class ProductRateExtension
{
    public static ProductRateResponseDTO ToProductRateResponse(this ProductRate productRate, Product product)
    {
        return new ProductRateResponseDTO()
        {
            productId = productRate.ProductId,
            ProductRateId = productRate.ProductId,
            Rate = productRate.Rate,
            EffectiveDate = productRate.EffectiveDate,
            ProductName = product.Name,
        };
    }
}
