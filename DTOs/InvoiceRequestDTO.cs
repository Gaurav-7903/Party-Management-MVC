namespace Party_Management.DTOs
{
    public class InvoiceRequestDTO
    {
        public int PartyId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
