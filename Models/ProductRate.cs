using Party_Management.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Party_Management.Models
{
    public class ProductRate
    {
        [Key]
        public int ProductRateId { get; set; }


        [Required(ErrorMessage = "Product Id is Required")]
        public int ProductId { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        [Required(ErrorMessage = "Product Rate is Required")]
        [Range(0.01, 999999.99, ErrorMessage = "Rate must be greater than 0 and less than 1,000,000.")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DateNotInFuture(ErrorMessage = "Effective Date cannot be in the future.")]
        public DateTime EffectiveDate { get; set; } = DateTime.Now;
    }

}
