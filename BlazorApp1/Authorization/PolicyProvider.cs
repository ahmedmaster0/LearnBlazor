using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BlazorApp1.Authorization
{
    public class PolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public PolicyProvider(IOptions<AuthorizationOptions> options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {

            // Check for static policies first in middleware
            var policy = await base.GetPolicyAsync(policyName);
            if (policy != null)
            {
                return policy;
            }

            // Fetch the policy from the database
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //    var policyEntity = await _userManager.GetClaimsAsync()
            //    if (policyEntity != null)
            //    {
            //        var policyBuilder = new AuthorizationPolicyBuilder();
            //        policyBuilder.RequireClaim(policyEntity.ClaimType, policyEntity.ClaimValue); 
            //        Console.WriteLine($"Policy '{policyName}' loaded from database."); 
            //        return policyBuilder.Build();
            //    }
            //}

            // if no policy
            return null;
        }

    }
}
