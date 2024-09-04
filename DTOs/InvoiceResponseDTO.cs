namespace Party_Management.DTOs
{
    public class InvoiceResponseDTO
    {
        public int InvoiceId { get; set; }

        public int PartyId { get; set; }

        public string? PartyName { get; set; }

        public int ProductCount { get; set; }

        public decimal Total { get; set; }

        public DateTime InvoiceDate { get; set; }
    }
}
