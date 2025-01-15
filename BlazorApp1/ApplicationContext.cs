using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
