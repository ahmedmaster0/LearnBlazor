using Microsoft.AspNetCore.Authorization;

namespace BlazorApp1.Authorization
{
    public class CustomHandler : AuthorizationHandler<CustomRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            if (context.User.HasClaim(m => m.Type == "Permission"))
            {
               string _values = context.User.Claims.FirstOrDefault(m => m.Type.Equals("Permission")).Value;
                List<string> lst_claims = _values.Split(',').ToList();
                if(lst_claims.Any(m => m.Equals(requirement.Permission)))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
