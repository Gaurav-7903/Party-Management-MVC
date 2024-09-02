using Microsoft.AspNetCore.Mvc;
using Party_Management.Enums;
using System.ComponentModel.DataAnnotations;

namespace Party_Management.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name can't be blank")]
        public string PersonName { get; set; }


        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [Remote(action: "IsEmailAlreadyRegister", controller: "Account", ErrorMessage = "Email is Already Register")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "PassWord and Confirm Password Not Match")]
        public string ConfirmPassword { get; set; }

        public UserRoleOptions UserType { get; set; } = UserRoleOptions.User;
    }
}
