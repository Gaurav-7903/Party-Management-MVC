namespace Party_Management.DTOs
{
    public class ProductRateAndProductDTO
    {
        public ProductResponseDTO ProductResponseDTO { get; set; }

        public IEnumerable<ProductRateResponseDTO> productRateResponseDTO { get; set; }

        public ProductRateRequestDTO? ProductRateRequestDTO { get; set; }
    }
}
