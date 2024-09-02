using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Party_Management.Models
{
    public class InvoiceDetails
    {
        [Key]
        public int InvoiceDetailsId { get; set; }


        [Required(ErrorMessage = "Invoice Id is Required")]
        public int InvoiceId { get; set; }


        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }


        [Required(ErrorMessage = "ProductId is Required")]
        public int ProductId { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Price is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public int Price { get; set; }
    }

}
