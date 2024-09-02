using Microsoft.AspNetCore.Identity;

namespace Party_Management.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
