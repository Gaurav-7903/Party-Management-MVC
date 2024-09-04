using System.ComponentModel.DataAnnotations;

namespace Party_Management.Models
{
    public class Party
    {
        [Key]
        public int PartyId { get; set; }

        [Required(ErrorMessage = "Party Name is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Party Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Party Name can only contain letters and spaces.")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        public bool ReceiveNotification { get; set; }

        // navigation properties
        public ICollection<PartyAssignment>? PartyAssignments { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}
