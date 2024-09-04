using Party_Management.Validation;
using ServiceContract;
using System.ComponentModel.DataAnnotations;

namespace Party_Management.Validation
{
    public class PartyEmailNotRegisteredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Email is Empty");
            }
            var emailAddress = value.ToString();

            var _partyService = validationContext.GetService<IPartyService>();
            var partyById = _partyService.GetPartyByEmail(emailAddress);
            if (partyById == null)
            {
                return ValidationResult.Success; // Email is not registered, validation passes
            }

            return new ValidationResult("Email is already registered.");
        }
    }
}

