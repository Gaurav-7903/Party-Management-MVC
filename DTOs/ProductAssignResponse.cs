namespace Party_Management.DTOs
{
    public class ProductAssignResponse
    {
        public int ProductAssignId { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int PartyID { get; set; }

        public string? PartyName { get; set; }
    }
}
