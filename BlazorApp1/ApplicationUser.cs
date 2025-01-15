using Microsoft.AspNetCore.Identity;

namespace BlazorApp1
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
