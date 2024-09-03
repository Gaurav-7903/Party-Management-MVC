using Party_Management.DTOs;

namespace Party_Management.ServiceContract
{
    public interface IProductRateService
    {
        public IEnumerable<ProductRateResponseDTO> GetProductRate();
        public IEnumerable<ProductRateResponseDTO> GetProductRateById(int productId);
        public ProductRateResponseDTO ChangeProductRate(ProductRateRequestDTO productRateRequestDTO);
    }
}
