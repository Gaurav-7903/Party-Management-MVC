namespace Party_Management.DTOs
{
    public class ProductAssignDTO
    {
        public PartyResponseDTO PartyResponseDTO { get; set; }

        public IEnumerable<ProductResponseDTO> ProductResponseDTOs { get; set; }

        public ProductAssignmentRequest? ProductAssignmentRequest { get; set; }

    }
}
