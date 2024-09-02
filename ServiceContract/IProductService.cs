using Party_Management.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    public interface IProductService
    {
        public ProductResponseDTO CreateProduct(ProductRequestDTO productRequestDTO);

        public ProductResponseDTO UpdateProduct(ProductResponseDTO productResponseDTO);

        public IEnumerable<ProductResponseDTO> GetAllProducts();

        public bool DeleteProduct(int ProductId);

        public ProductResponseDTO GetProductById(int ProductId);
    }
}
