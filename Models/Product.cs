using System.ComponentModel.DataAnnotations;

namespace Party_Management.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<ProductRate>? ProductRates { get; set; }
        public ICollection<PartyAssignment>? ProductAssignments { get; set; }
        public ICollection<InvoiceDetails>? InvoiceDetails { get; set; }
    }
}
