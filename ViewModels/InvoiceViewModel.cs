using Party_Management.DTOs;

namespace Party_Management.ViewModels
{
    public class InvoiceViewModel
    {
        public int PartyId { get; set; }

        public string PartyName { get; set; }

        public int SelectedProductId { get; set; }

        public int SelectedPartyId { get; set; }

        //public IEnumerable<ProductResponseDTO> Products { get; set; }
    }
}
