using System.ComponentModel.DataAnnotations;

namespace Party_Management.DTOs
{
    public class CustomDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue < DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be today or in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }

    // ProductAssign 
    public class ProductRateRequestDTO
    {
        [Required(ErrorMessage = "ProductId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be a positive integer greater than zero.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than zero.")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "EffectiveDate is required.")]
        [DataType(DataType.Date)]
        [CustomDateRange(ErrorMessage = "EffectiveDate must be a present or future date.")]
        public DateTime EffectiveDate { get; set; }
    }
}
