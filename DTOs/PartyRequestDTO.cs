using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Party_Management.Validation;

namespace Party_Management.DTOs
{
    public class PartyRequestDTO
    {
        [Required(ErrorMessage = "Party name is required.")]
        [StringLength(100, ErrorMessage = "Party name can't be longer than 100 characters.")]
        [AlphaOnlyLength(2, 50, ErrorMessage = "Party name can only contain alphabetic characters.")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [PartyEmailNotRegistered(ErrorMessage = "Email is already registered.")]
        public string EmailAddress { get; set; }

        public bool ReceiveNotifications { get; set; } = false;
    }
}
