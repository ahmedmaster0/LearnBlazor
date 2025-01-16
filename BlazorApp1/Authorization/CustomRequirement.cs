using Microsoft.AspNetCore.Authorization;

namespace BlazorApp1.Authorization
{
    public class CustomRequirement : IAuthorizationRequirement
    {
        public string Permission { get; set; }

        public CustomRequirement(string _permission)
        {
            Permission = _permission;
        }


    }
}
