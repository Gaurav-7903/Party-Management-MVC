using Party_Management.DTOs;
using Party_Management.Models;

namespace Party_Management.ServiceContract
{
    public interface IProductAssignmentService
    {
        public IEnumerable<ProductResponseDTO> GetAssignProductByPartyID(int partyId);

        public ProductAssignResponse AssignProductToParty(int partyId, int ProductId);

        public IEnumerable<ProductResponseDTO> GetNotAssignedProduct(int partyId);

        public IEnumerable<ProductAssignResponse> GetAllAssignProductAndPArty();
    }
}
