using System.ComponentModel.DataAnnotations;

namespace Party_Management.DTOs
{
    public class ProductRequestDTO
    {
        [Required(ErrorMessage = "Product Name Cant be Empty")]
        [AlphaOnlyLength(2, 50, ErrorMessage = "Product Name must be between 2 and 50 characters and only contain letters.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Product Name must be between 2 and 50 characters.")]
        public string ProuctName { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid decimal number with up to 2 decimal places.")]
        public decimal Price { get; set; }
    }
}
