using Microsoft.AspNetCore.Identity;

namespace BlazorApp1.SeedData
{
    public static class SeedRoles
    {
        public static async Task<bool> AddRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    var res1 = await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    });

                    var res2 = await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    });

                }
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
       
        }
    }
}
