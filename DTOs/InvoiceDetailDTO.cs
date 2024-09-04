namespace Party_Management.DTOs
{
    public class InvoiceDetailDTO
    {
        public int InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int PartyId { get; set; }

        public string PartyName { get; set; }

        public IEnumerable<InvoiceItemDTO> invoiceItems { get; set; }

    }
}
