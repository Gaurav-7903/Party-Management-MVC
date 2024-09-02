using System.ComponentModel.DataAnnotations;


namespace Party_Management.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email Is Not Provided")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Not Provided")]
        public string Password { get; set; }
    }
}
