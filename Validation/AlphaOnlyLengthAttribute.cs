using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class AlphaOnlyLengthAttribute : ValidationAttribute
{
    public int MinimumLength { get; set; }
    public int MaximumLength { get; set; }

    public AlphaOnlyLengthAttribute(int minimumLength, int maximumLength)
    {
        MinimumLength = minimumLength;
        MaximumLength = maximumLength;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var strValue = value as string;

        if (strValue != null)
        {
            if (strValue.Length < MinimumLength || strValue.Length > MaximumLength)
            {
                return new ValidationResult($"The field must be between {MinimumLength} and {MaximumLength} characters long.");
            }

            if (!Regex.IsMatch(strValue, "^[a-zA-Z]+$"))
            {
                return new ValidationResult("The field can not contain letters.");
            }
        }

        return ValidationResult.Success;
    }
}
